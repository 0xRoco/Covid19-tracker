using covid19_tracker.ViewModels;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
        private jsonParse.News _news = new jsonParse.News();
        private const string TrackerDataFile = "AllInfectedCountries.json";
        private const string NewsDataFile = "NewsArticles.json";

        private const string NewsCountry = "us";
        private const string NewsLanguage = "en";
        public MainWindow()
        {
            InitializeComponent();
            DataContext = _vm ?? new ObjectVm();
            if (_vm != null)
            {
                _vm.CountryVm = new List<BaseModel.Country>();
                _vm.NewsVm = new List<BaseModel.News>();
                _vm.WorldwideVm = new BaseModel.Worldwide();
                _vm.Vm = new BaseModel();
            }

            PrepareData();
        }

        private async void PrepareData()
        {
            ReadTrackerJson();
            await ReadNewsJson();
            _track.Response.Sort((response, response1) => response1.Cases.Total.CompareTo(response.Cases.Total));
            foreach (var c in _track.Response)
                if (c.Country == "World" || c.Country == "All")
                {
                    _vm.WorldwideVm.NewCases = c.Cases.New;
                    _vm.WorldwideVm.Active = c.Cases.Active;
                    _vm.WorldwideVm.Critical = c.Cases.Critical;
                    _vm.WorldwideVm.Recovered = c.Cases.Recovered;
                    _vm.WorldwideVm.TotalCases = c.Cases.Total;
                    _vm.WorldwideVm.NewDeaths = c.Deaths.New;
                    _vm.WorldwideVm.TotalDeaths = c.Deaths.Total;
                    _vm.WorldwideVm.DeathRate =
                        CalculateDeathRate(_vm.WorldwideVm.TotalDeaths, _vm.WorldwideVm.TotalCases);
                }
                else
                {
                    _vm.CountryVm.Add(new BaseModel.Country
                    {
                        Name = c.Country, NewCases = c.Cases.New, Active = c.Cases.Active, Critical = c.Cases.Critical,
                        Recovered = c.Cases.Recovered, TotalCases = c.Cases.Total, NewDeaths = c.Deaths.New,
                        TotalDeaths = c.Deaths.Total, DeathRate = CalculateDeathRate(c.Deaths.Total, c.Cases.Total)
                    });
                }

            BindData();
            await Task.Run(UpdateTimer);
        }

        private bool IsTimeToUpdate()
        {
            var ts = DateTime.Now - _vm.Vm.UpdateTime;
            if (!(ts.TotalSeconds >= MaxUpdateTime))
            {
                return false;
            }
            _vm.Vm.UpdateTime = DateTime.Now;
            return true;
        }
        private async Task UpdateTimer()
        {
            while (true)
            {
                if (!IsTimeToUpdate())
                {
                    await Task.Delay(500); continue;

                }
                await Task.Run(UpdateTrackerApiData);
                await Task.Run(UpdateNewsApiData);
            }
        }

        private void BindData()
        {
            TotalConfirmedList.ItemsSource = _vm.CountryVm;
            TotalDeathsList.ItemsSource = _vm.CountryVm;
            TotalRecoveredList.ItemsSource = _vm.CountryVm;
            TotalCases.DataContext = _vm.WorldwideVm;
            TotalDeaths.DataContext = _vm.WorldwideVm;
            TotalRecovered.DataContext = _vm.WorldwideVm;
            //Updatetime.DataContext = _vm.Vm;
            SelectedCountry.DataContext = _vm.CountryVm[0];
            Worldwide.DataContext = _vm.WorldwideVm;
            BindNewsData();

        }

        private void BindNewsData()
        {
            try
            {
                var grids = new List<Grid>(11) {card1,card2,card3,card4,card5,card6,card7,card8,card9,card10,card11};
                for (var i = 0; i < grids.Count; i++)
                {
                    if (_vm.NewsVm[i] != null)
                        grids[i].DataContext = _vm.NewsVm[i];
                }

            }
            catch(Exception ex)
            {
                Task.Run(() =>
                    MessageBox.Show($"An unknown error has been detected\nCode: {ex.HResult}\n{ex.Message}","Oops",MessageBoxButton.OK,MessageBoxImage.Error));
            }
        }
        private static double CalculateDeathRate(int deaths, int confirmedCases)
        {
            return Math.Round(deaths / (double) confirmedCases * 100, 2);
        }

        private async Task ReadNewsJson()
        {
            while (true)
            {
                if (File.Exists(NewsDataFile))
                {
                    if (new FileInfo(NewsDataFile).Length != 0)
                    {
                        var jsonraw = File.ReadAllText(NewsDataFile);
                        _news = JsonConvert.DeserializeObject<jsonParse.News>(jsonraw);
                        await UpdateNewsData();
                    }
                    else
                    {
                        await UpdateNewsApiData();
                        continue;
                    }
                }
                else
                {
                    var fc = File.Create(NewsDataFile);
                    fc.Close();
                    await UpdateNewsApiData();
                    continue;
                }
                break;
            }
        }


        private async Task UpdateNewsApiData()
        {
            _client = new RestClient($"http://newsapi.org/v2/top-headlines?country={NewsCountry}&q=coronavirus&covid19&language={NewsLanguage}&apiKey=3a0fa4d73e4349fc9847ae22da3ede58");
            var request = new RestRequest(Method.GET);
            var response = _client.Execute(request);
            _news = JsonConvert.DeserializeObject<jsonParse.News>(response.Content);
            File.WriteAllText(NewsDataFile, response.Content);
            await UpdateNewsData();
        }

        private async Task UpdateNewsData()
        {
            await Task.Delay(500);
            _vm.NewsVm.Clear();
            foreach (var article in _news.Articles)
                _vm.NewsVm.Add(new BaseModel.News
                {
                    Author = article.Author, Title = article.Title, Description = article.Description,
                    Url = article.Url, UrlToImage = article.UrlToImage, PublishedAt = article.PublishedAt.ToLocalTime(),
                    Content = article.Content, Source = article.Source.Name
                });
            var invokeAction = new Action(() => { NewsBadge.Badge = "NEW"; });
                await NewsBadge.Dispatcher.BeginInvoke(
                    invokeAction
                );
            }

        private async void ReadTrackerJson()
        {
            while (true)
            {
                if (File.Exists(TrackerDataFile))
                {
                    if (new FileInfo(TrackerDataFile).Length != 0)
                    {
                        _vm.Vm.UpdateTime = File.GetLastWriteTime(TrackerDataFile);
                        var jsonraw = File.ReadAllText(TrackerDataFile);
                        _track = JsonConvert.DeserializeObject<jsonParse.Tracker>(jsonraw);
                    }
                    else
                    {
                        await UpdateTrackerApiData();
                        continue;
                    }
                }
                else
                {
                    var fc = File.Create(TrackerDataFile);
                    fc.Close();
                    await UpdateTrackerApiData();
                    continue;
                }

                break;
            }
        }

        private async Task UpdateTrackerData()
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
            _vm.WorldwideVm.TotalCases = _track.Response[_allIndex].Cases.Total;
            _vm.WorldwideVm.Active = _track.Response[_allIndex].Cases.Active;
            _vm.WorldwideVm.Critical = _track.Response[_allIndex].Cases.Critical;
            _vm.WorldwideVm.Recovered = _track.Response[_allIndex].Cases.Recovered;
            _vm.WorldwideVm.NewCases = _track.Response[_allIndex].Cases.New;
            _vm.WorldwideVm.NewDeaths = _track.Response[_allIndex].Deaths.New;
            _vm.WorldwideVm.TotalDeaths = _track.Response[_allIndex].Deaths.Total;
            _vm.WorldwideVm.DeathRate = CalculateDeathRate(_vm.WorldwideVm.TotalDeaths, _vm.WorldwideVm.TotalCases);
        }

        private async Task UpdateTrackerApiData()
        {
            _client = new RestClient("https://covid-193.p.rapidapi.com/statistics");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "covid-193.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "db654bb9eemshe5a8718dd1418ffp1b94abjsna0a6321ba60a");
            var response = _client.Execute(request);
            _track = JsonConvert.DeserializeObject<jsonParse.Tracker>(response.Content);
            File.WriteAllText(TrackerDataFile, response.Content);
            await UpdateTrackerData();
        }

        private void CList_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = ((ListView) sender).SelectedItem;
            if (item == null) return;
            var itemId = TotalConfirmedList.Items.IndexOf(item);
            SelectedCountry.DataContext = _vm.CountryVm[itemId];
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var index = int.Parse(((Button) e.Source).Uid);
            Transitioner.SelectedIndex = index;
            switch (index)
            {
                case 0:
                    break;
                case 1:
                    NewsBadge.Badge = "";
                    break;
            }
        }

        private void ButtonTop_OnClick(object sender, RoutedEventArgs e)
        {
            var index = int.Parse(((Button) e.Source).Uid);
            switch (index)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    WindowState = WindowState.Minimized;
                    break;
                case 3:
                    Close();
                    break;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ShareButton_OnClick(object sender, RoutedEventArgs e)
        {
            var index = int.Parse(((Button) e.Source).Uid);
            if (index < 0 || index >= _vm.NewsVm.Count) return;
            if (_vm.NewsVm[index] != null)
            {
                Process.Start(_vm.NewsVm[index].Url);
            }
        }
    }

    public class ObjectVm
    {
        public BaseModel Vm { get; set; }
        public List<BaseModel.Country> CountryVm { get; set; }
        public BaseModel.Worldwide WorldwideVm { get; set; }
        public List<BaseModel.News> NewsVm { get; set; }
    }
}