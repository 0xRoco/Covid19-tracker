﻿using System;

namespace covid19_tracker.ViewModels
{
    public class Base
    {
        public DateTime UpdateTime { get; set; }
        public int NextUpdateIn { get; set; }
        public int SelectedLanguage { get; set; }

        public class Country
        {
            public string Name { get; set; }
            public string NewCases { get; set; }
            public int Active { get; set; }
            public int Critical { get; set; }
            public int Recovered { get; set; }
            public int TotalCases { get; set; }
            public string NewDeaths { get; set; }
            public int TotalDeaths { get; set; }
            public double DeathRate { get; set; }
        }

        public class Worldwide
        {
            public string NewCases { get; set; }
            public int Active { get; set; }
            public int Critical { get; set; }
            public int Recovered { get; set; }
            public int TotalCases { get; set; }
            public string NewDeaths { get; set; }
            public int TotalDeaths { get; set; }
            public double DeathRate { get; set; }
        }

        public class News
        {
            public string Author { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Url { get; set; }
            public string UrlToImage { get; set; }
            public DateTime PublishedAt { get; set; }
            public string Content { get; set; }
            public string Source { get; set; }
        }

        public class Localisation
        {
            public string HomeBtn { get; set; }
            public string NewsBtn { get; set; }
            public string TotalConfirmedText { get; set; }
            public string TotalDeathsText { get; set; }
            public string TotalRecoveredText { get; set; }
            public string ActiveText { get; set; }
            public string CriticalText { get; set; }
            public string MortalityRateText { get; set; }
            public string WorldwideStatisticsText { get; set; }
        }
    }
}