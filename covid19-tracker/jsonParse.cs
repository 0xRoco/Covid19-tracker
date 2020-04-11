using System;
using System.Collections.Generic;

namespace covid19_tracker
{
    internal class jsonParse
    {
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


        public class Source
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }

        public class Article
        {
            public Source Source { get; set; }
            public string Author { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Url { get; set; }
            public string UrlToImage { get; set; }
            public DateTime PublishedAt { get; set; }
            public string Content { get; set; }
        }

        public class News
        {
            public List<Article> Articles { get; set; }
        }
    }
}