using System;

namespace covid19_tracker.ViewModels
{
    public class Base
    {
        public DateTime UpDateTime { get; set; }
        public int nextUpdateIn { get; set; }

        public class Country
        {
            public string name { get; set; }
            public string NewCases { get; set; }
            public int Active { get; set; }
            public int Critical { get; set; }
            public int Recovered { get; set; }
            public int TotalCases { get; set; }
            public string NewDeaths { get; set; }
            public int TotalDeaths { get; set; }
            public double deathRate { get; set; }
        }

        public class Worldwide
        {
            public string wwNewCases { get; set; }
            public int wwActive { get; set; }
            public int wwCritical { get; set; }
            public int wwRecovered { get; set; }
            public int wwTotalCases { get; set; }
            public string wwNewDeaths { get; set; }
            public int wwTotalDeaths { get; set; }
            public double wwDeathRate { get; set; }
        }
    }
}