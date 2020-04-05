using System;
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
        private DateTime _timeSinceLastUpdate = DateTime.Now;
        private int allIndex;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = _vm ?? new ObjectVm();
            _vm.WorldwideVm = new BaseModel.Worldwide();
            _vm.CountryVm = new List<BaseModel.Country>();
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
            cList.ItemsSource = _vm.CountryVm;
        }
        private void ReadFromRawJsonFile()
        {
            _timeSinceLastUpdate = File.GetLastWriteTime("AllInfectedCountries.json");
            TimeSpan ts = DateTime.Now - _timeSinceLastUpdate;
            if (ts.TotalSeconds >= 30)
            {
                  Task.Run(ApiUpdateData);
            }
            var jsonraw = File.ReadAllText("AllInfectedCountries.json");
            track = JsonConvert.DeserializeObject<jsonParse.Tracker>(jsonraw);
        }

        private async Task UpdateData()
        {
            await Task.Delay(500);
            allIndex = track.Response.FindIndex(x => x.Country == "All");
            foreach (var c in track.Response.Where(x => !x.Country.Equals("All")))
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
            await Task.Delay(500);
            _client = new RestClient("https://covid-193.p.rapidapi.com/statistics");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "covid-193.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "db654bb9eemshe5a8718dd1418ffp1b94abjsna0a6321ba60a");
            var response = _client.Execute(request);
            track = JsonConvert.DeserializeObject<jsonParse.Tracker>(response.Content);
            File.WriteAllText(@"./AllInfectedCountries.json", response.Content);
            await Task.Run(UpdateData);
        }
    }

    public class ObjectVm
    {
        public List<BaseModel.Country> CountryVm { get; set; }
        public BaseModel.Worldwide WorldwideVm { get; set; }
    }
}
