using Newtonsoft.Json;
using RestSharp;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace covid19_tracker
{
    internal class Program
    {
        private static RestClient _client = new RestClient();
        private static jsonParse.Tracker _track = new jsonParse.Tracker();
        private static DateTime _timeSinceLastUpdate = DateTime.Now;
        private static int _allIndex;
        private static int _maxUpdatetime = 1800;
        private static void Main()
        {
            //DeserializeObject<jsonParse.Tracker>(rawjson);
            //apiGetAllInfectedCountries();
            Task.Run((() => UpdateTimer()));
            string jsonraw = File.ReadAllText("AllInfectedCountries.json");
            _track = JsonConvert.DeserializeObject<jsonParse.Tracker>(jsonraw);
            foreach (var response in _track.Response.Where(x => x.Country=="All")) _allIndex = _track.Response.IndexOf(response);
            while (true)
            {
                var command = Console.ReadLine();
                var cIndex=0;
                if (command == null) continue;
                if (command.StartsWith("info"))
                {
                    var country = command.Remove(0, command.IndexOf(' ') + 1).ToLower();
                    foreach (var response in _track.Response.Where(x => x.Country.ToLower().StartsWith(country)))
                    {
                        cIndex = _track.Response.IndexOf(response);
                    }
                    //Logs($"[Debug] Searched: {country} GOT {track.Response[c_index].Country}");
                    Logs($"[{_track.Response[cIndex].Country}] Total infected: {_track.Response[cIndex].Cases.Total} - Total recoveries: {_track.Response[cIndex].Cases.Recovered} - Total deaths: {_track.Response[cIndex].Deaths.Total}");
                }else if (command.ToLower().Equals("--fupdate"))
                {
                    //Task.Run((() => UpdateTimer(true)));
                }else if (command.ToLower().Equals("--settings"))
                {

                }else if (command.ToLower().StartsWith("--com"))
                {

                }
                else
                {
                    Logs("UNKNOWN COMMAND!",0);
                }
            }
        }

        private static async Task UpdateTimer()
        {
            //Logs($"[Debug] Update timer started, time since last update : {ts.TotalSeconds}");
            while (true)
            {
                var ts = DateTime.Now - _timeSinceLastUpdate;
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                if (!(ts.TotalSeconds >= _maxUpdatetime))
                {
                     //Logs($"[Debug] {Convert.ToInt32(ts.TotalSeconds).ToString()}/{_maxUpdatetime} next update timer progress!"); 
                     await Task.Delay(5000); continue;}
                _timeSinceLastUpdate = DateTime.Now;
                await ApiGetAllInfectedCountries();
                Logs("Updated all countries", 1);
                Logs(
                    $"[Worldwide-{_track.Response.Count - 1}] Total cases {_track.Response[_allIndex].Cases.Total} {_track.Response[_allIndex].Cases.New} new cases - total deaths {_track.Response[_allIndex].Deaths.Total} {_track.Response[_allIndex].Deaths.New} new deaths ");
                /* foreach (var c in track.Response.Where(c => c.Cases.New != null))
                        {
                            Logs($"{c.Country} has {c.Cases.New} cases");
                        }*/
            }
        }

        static async Task ApiGetAllInfectedCountries()
        {
            await Task.Delay(500);
            _client = new RestClient("https://covid-193.p.rapidapi.com/statistics");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "covid-193.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "db654bb9eemshe5a8718dd1418ffp1b94abjsna0a6321ba60a");
            IRestResponse response = _client.Execute(request);
            _track = JsonConvert.DeserializeObject<jsonParse.Tracker>(response.Content);
            File.WriteAllText(@"./AllInfectedCountries.json",response.Content);
        }
        static void Logs(string text, int code=-1)
        {
            switch (code)
            {
                case 0:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case 1:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                default:
                    Console.ResetColor();
                    break;

            }
            Console.WriteLine($"{DateTime.Now:f} - {text}");
            Console.ResetColor();
        }
    }
}
