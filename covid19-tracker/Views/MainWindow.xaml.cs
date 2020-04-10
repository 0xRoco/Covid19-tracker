using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using covid19_tracker.ViewModels;
using Newtonsoft.Json;
using RestSharp;

namespace covid19_tracker
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int MaxUpdateTime = 900;
        private readonly ObjectVm _vm = new ObjectVm();
        private int _allIndex;
        private RestClient _client = new RestClient();
        private jsonParse.Tracker _track = new jsonParse.Tracker();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _vm ?? new ObjectVm();
            if (_vm != null)
            {
                _vm.WorldwideVm = new BaseModel.Worldwide();
                _vm.CountryVm = new List<BaseModel.Country>();
                _vm.Vm = new BaseModel();
            }

            PrepareData();
        }

        private void PrepareData()
        {
            ReadFromRawJsonFile();
            _track.Response.Sort((response, response1) => response1.Cases.Total.CompareTo(response.Cases.Total));
            foreach (var c in _track.Response)
                if (c.Country == "World" || c.Country == "All")
                {
                    _vm.WorldwideVm.wwNewCases = c.Cases.New;
                    _vm.WorldwideVm.wwActive = c.Cases.Active;
                    _vm.WorldwideVm.wwCritical = c.Cases.Critical;
                    _vm.WorldwideVm.wwRecovered = c.Cases.Recovered;
                    _vm.WorldwideVm.wwTotalCases = c.Cases.Total;
                    _vm.WorldwideVm.wwNewDeaths = c.Deaths.New;
                    _vm.WorldwideVm.wwTotalDeaths = c.Deaths.Total;
                }
                else
                {
                    _vm.CountryVm.Add(new BaseModel.Country
                    {
                        Name = c.Country, NewCases = c.Cases.New, Active = c.Cases.Active, Critical = c.Cases.Critical,
                        Recovered = c.Cases.Recovered, TotalCases = c.Cases.Total, NewDeaths = c.Deaths.New,
                        TotalDeaths = c.Deaths.Total
                    });
                }

            BindData();
            Task.Run(UpdateTimer);
        }

        //TODO: Fix UpdateTimer,UpdateData,ApiUpdateData to use TASK instead of method 
        //BUG: DATA DOESN'T UPDATE IN UI AFTER BEING UPDATED HERE USING TASKS DOESN'T HELP EITHER

        private async Task UpdateTimer()
        {
            while (true)
            {
                var ts = DateTime.Now - _vm.Vm.UpDateTime;
                if (!(ts.TotalSeconds >= MaxUpdateTime))
                {
                    await Task.Delay(500);
                    continue;
                }

                await ApiUpdateData();
                _vm.Vm.UpDateTime = DateTime.Now;
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
            Updatetime.DataContext = _vm.Vm;
            selectedCountry.DataContext = _vm.CountryVm[0];
            selectedCountryExtra.DataContext = _vm.CountryVm[0];
            SelectedCountryInfo.DataContext = _vm.CountryVm[0];
        }

        private async void ReadFromRawJsonFile()
        {
            while (true)
            {
                if (File.Exists("AllInfectedCountries.json"))
                {
                    if (new FileInfo("AllInfectedCountries.json").Length != 0)
                    {
                        _vm.Vm.UpDateTime = File.GetLastWriteTime("AllInfectedCountries.json");
                        var jsonraw = File.ReadAllText("AllInfectedCountries.json");
                        _track = JsonConvert.DeserializeObject<jsonParse.Tracker>(jsonraw);
                    }
                    else
                    {
                        await ApiUpdateData();
                        continue;
                    }
                }
                else
                {
                    var fc = File.Create("AllInfectedCountries.json");
                    fc.Close();
                    await ApiUpdateData();
                    continue;
                }

                break;
            }
        }

        private async Task UpdateData()
        {
            await Task.Delay(500);
            _allIndex = _track.Response.FindIndex(x => x.Country == "All");
            foreach (var c in _track.Response.Where(x => !x.Country.Equals("All") && !x.Country.Equals("World")))
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

        private async Task ApiUpdateData()
        {
            _client = new RestClient("https://covid-193.p.rapidapi.com/statistics");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "covid-193.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "db654bb9eemshe5a8718dd1418ffp1b94abjsna0a6321ba60a");
            var response = _client.Execute(request);
            _track = JsonConvert.DeserializeObject<jsonParse.Tracker>(response.Content);
            File.WriteAllText(@"./AllInfectedCountries.json", response.Content);
            await UpdateData();
        }

        private void CList_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = ((ListView) sender).SelectedItem;
            if (item == null) return;
            var itemId = cList.Items.IndexOf(item);
            selectedCountry.DataContext = _vm.CountryVm[itemId];
            selectedCountryExtra.DataContext = _vm.CountryVm[itemId];
            SelectedCountryInfo.DataContext = _vm.CountryVm[itemId];
            _vm.Vm.nextUpdateIn++;
        }
    }

    public class ObjectVm
    {
        public BaseModel Vm { get; set; }
        public List<BaseModel.Country> CountryVm { get; set; }
        public BaseModel.Worldwide WorldwideVm { get; set; }
    }
}