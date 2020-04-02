using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace covid19_tracker
{
    public class jsonParse
    {
        public class Parameters
        {
            public string Country { get; set; }
        }

        public class Cases
        {
            public string New { get; set; }
            public int Active { get; set; }
            public int Critical { get; set; }
            public int Recovered { get; set; }
            public int Total { get; set; }
        }

        public class Deaths
        {
            public string New { get; set; }
            public int Total { get; set; }
        }

        public class Response
        {
            public string Country { get; set; }
            public Cases Cases { get; set; }
            public Deaths Deaths { get; set; }
            public string Day { get; set; }
            public DateTime Time { get; set; }
        }

        public class Tracker
        {
            public List<Response> Response { get; set; } 
        }
    }
}
