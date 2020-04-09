﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using covid19_trackerGUI.ViewModels;
using Newtonsoft.Json;
using RestSharp;

namespace covid19_trackerGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RestClient _client = new RestClient();
        private ObjectVm _vm = new ObjectVm();
        private jsonParse.Tracker track = new jsonParse.Tracker();
        private int allIndex;
        private int _maxUpdateTime = 10;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _vm ?? new ObjectVm();
            _vm.WorldwideVm = new BaseModel.Worldwide();
            _vm.CountryVm = new List<BaseModel.Country>();
            _vm.vm = new Base();
            PrepareData();
        }

        private void PrepareData()
        {
             ReadFromRawJsonFile();
            foreach (var c in track.Response)
            {
                if (c.Country == "World" || c.Country=="All")
                {
                    if (c.Country != "World") continue;
                    _vm.WorldwideVm.wwNewCases = c.Cases.New;
                    _vm.WorldwideVm.wwActive = c.Cases.Active;
                    _vm.WorldwideVm.wwCritical = c.Cases.Critical;
                    _vm.WorldwideVm.wwRecovered = c.Cases.Recovered;
                    _vm.WorldwideVm.wwTotalCases = c.Cases.Total;
                    _vm.WorldwideVm.wwNewDeaths = c.Deaths.New;
                    _vm.WorldwideVm.wwTotalDeaths = c.Deaths.Total;
                }else
                    _vm.CountryVm.Add(new BaseModel.Country() { Name = c.Country , NewCases = c.Cases.New , Active = c.Cases.Active, Critical = c.Cases.Critical, Recovered = c.Cases.Recovered, TotalCases = c.Cases.Total, NewDeaths = c.Deaths.New, TotalDeaths = c.Deaths.Total});
            }
            BindData();
            UpdateTimer();
        }

        //TODO: Fix UpdateTimer,UpdateData,ApiUpdateData to use TASK instead of method 
        //BUG: DATA DOESN'T UPDATE IN UI AFTER BEING UPDATED HERE USING TASKS DOESN'T HELP EITHER

        private async void UpdateTimer()
        {
            while (true)
            {
                var ts = DateTime.Now - _vm.vm.UpDateTime;
                if (!(ts.TotalSeconds >= _maxUpdateTime))
                {
                    await Task.Delay(500); continue;}
                ApiUpdateData();
                UpdateData();
                _vm.vm.UpDateTime = DateTime.Now;
            }


        }
        private void BindData()
        {
            cList.ItemsSource = _vm.CountryVm;
            cListDeaths.ItemsSource = _vm.CountryVm;
            cListRecoveries.ItemsSource = _vm.CountryVm;
            TotalCases.DataContext = _vm.WorldwideVm;
            TotalDeaths.DataContext = _vm.WorldwideVm;
            TotalRecoveries.DataContext = _vm.WorldwideVm;
            Updatetime.DataContext = _vm.vm;
            selectedCountry.DataContext = _vm.CountryVm[0];
            selectedCountryExtra.DataContext = _vm.CountryVm[0];
            SelectedCountryInfo.DataContext = _vm.CountryVm[0];
        }
        private void ReadFromRawJsonFile()
        {
            _vm.vm.UpDateTime = File.GetLastWriteTime("AllInfectedCountries.json");
            var jsonraw = File.ReadAllText("AllInfectedCountries.json");
            track = JsonConvert.DeserializeObject<jsonParse.Tracker>(jsonraw);
        }
        private void UpdateData()
        {
            allIndex = track.Response.FindIndex(x => x.Country == "All");
            foreach (var c in track.Response.Where(x => !x.Country.Equals("All") && !x.Country.Equals("World")))
            {
                var tempindex = _vm.CountryVm.FindIndex(x => x.Name == c.Country);
                _vm.CountryVm[tempindex].NewCases = c.Cases.New;
                _vm.CountryVm[tempindex].Active = c.Cases.Active;
                _vm.CountryVm[tempindex].Critical = c.Cases.Critical;
                _vm.CountryVm[tempindex].Recovered = c.Cases.Recovered;
                _vm.CountryVm[tempindex].TotalCases = c.Cases.Total;
                _vm.CountryVm[tempindex].NewDeaths = c.Deaths.New;
                _vm.CountryVm[tempindex].TotalDeaths = c.Deaths.Total;
            }
        }
        private void ApiUpdateData()
        {
            _client = new RestClient("https://covid-193.p.rapidapi.com/statistics");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "covid-193.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "db654bb9eemshe5a8718dd1418ffp1b94abjsna0a6321ba60a");
            var response = _client.Execute(request);
            track = JsonConvert.DeserializeObject<jsonParse.Tracker>(response.Content);
            File.WriteAllText(@"./AllInfectedCountries.json", response.Content);
        }

        private void CList_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = ((ListView) sender).SelectedItem;
            if (item == null) return;
            var itemID = cList.Items.IndexOf(item);
            selectedCountry.DataContext = _vm.CountryVm[itemID];
            selectedCountryExtra.DataContext = _vm.CountryVm[itemID];
            SelectedCountryInfo.DataContext = _vm.CountryVm[itemID];
        }
    }

    public class ObjectVm
    {
        public Base vm { get; set; }
        public List<BaseModel.Country> CountryVm { get; set; }
        public BaseModel.Worldwide WorldwideVm { get; set; }
    }
}
