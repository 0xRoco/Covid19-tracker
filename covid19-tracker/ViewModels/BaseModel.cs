﻿using System;
using Jeuxjeux20.Mvvm;

namespace covid19_tracker.ViewModels
{
    public class BaseModel : ViewModelBase<Base>
    {
        public DateTime UpdateTime
        {
            get => Model.UpdateTime;
            set
            {
                Model.UpdateTime = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(UpdateTime));
            }
        }

        public int NextUpdateIn
        {
            get => Model.NextUpdateIn;
            set
            {
                Model.NextUpdateIn = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(NextUpdateIn));
            }
        }

        public int SelectedLanguage
        {
            get => Model.SelectedLanguage;
            set
            {
                Model.SelectedLanguage = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectedLanguage));
            }
        }

        public class Country : ViewModelBase<Base.Country>
        {
            public string Name
            {
                get => Model.Name;
                set
                {
                    Model.Name = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Name));
                }
            }

            public string NewCases
            {
                get => Model.NewCases;
                set
                {
                    Model.NewCases = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(NewCases));
                }
            }

            public int Active
            {
                get => Model.Active;
                set
                {
                    Model.Active = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Active));
                }
            }

            public int Critical
            {
                get => Model.Critical;
                set
                {
                    Model.Critical = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Critical));
                }
            }

            public int Recovered
            {
                get => Model.Recovered;
                set
                {
                    Model.Recovered = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Recovered));
                }
            }

            public int TotalCases
            {
                get => Model.TotalCases;
                set
                {
                    Model.TotalCases = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalCases));
                }
            }

            public string NewDeaths
            {
                get => Model.NewDeaths;
                set
                {
                    Model.NewDeaths = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(NewDeaths));
                }
            }

            public int TotalDeaths
            {
                get => Model.TotalDeaths;
                set
                {
                    Model.TotalDeaths = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalDeaths));
                }
            }

            public double DeathRate
            {
                get => Model.DeathRate;
                set
                {
                    Model.DeathRate = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(DeathRate));
                }
            }
        }

        public class Worldwide : ViewModelBase<Base.Worldwide>
        {
            public string NewCases
            {
                get => Model.NewCases;
                set
                {
                    Model.NewCases = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(NewCases));
                }
            }

            public int Active
            {
                get => Model.Active;
                set
                {
                    Model.Active = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Active));
                }
            }

            public int Critical
            {
                get => Model.Critical;
                set
                {
                    Model.Critical = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Critical));
                }
            }

            public int Recovered
            {
                get => Model.Recovered;
                set
                {
                    Model.Recovered = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Recovered));
                }
            }

            public int TotalCases
            {
                get => Model.TotalCases;
                set
                {
                    Model.TotalCases = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalCases));
                }
            }

            public string NewDeaths
            {
                get => Model.NewDeaths;
                set
                {
                    Model.NewDeaths = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(NewDeaths));
                }
            }

            public int TotalDeaths
            {
                get => Model.TotalDeaths;
                set
                {
                    Model.TotalDeaths = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalDeaths));
                }
            }

            public double DeathRate
            {
                get => Model.DeathRate;
                set
                {
                    Model.DeathRate = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(DeathRate));
                }
            }
        }

        public class News : ViewModelBase<Base.News>
        {
            public string Author
            {
                get => Model.Author;
                set
                {
                    Model.Author = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Author));
                }
            }

            public string Title
            {
                get => Model.Title;
                set
                {
                    Model.Title = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Title));
                }
            }

            public string Description
            {
                get => Model.Description;
                set
                {
                    Model.Description = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Description));
                }
            }

            public string Url
            {
                get => Model.Url;
                set
                {
                    Model.Url = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Url));
                }
            }

            public string UrlToImage
            {
                get => Model.UrlToImage;
                set
                {
                    Model.UrlToImage = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(UrlToImage));
                }
            }

            public DateTime PublishedAt
            {
                get => Model.PublishedAt;
                set
                {
                    Model.PublishedAt = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(PublishedAt));
                }
            }

            public string Content
            {
                get => Model.Content;
                set
                {
                    Model.Content = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Content));
                }
            }

            public string Source
            {
                get => Model.Source;
                set
                {
                    Model.Source = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Source));
                }
            }
        }

        public class Localisation : ViewModelBase<Base.Localisation>
        {
            public string HomeBtn
            {
                get => Model.HomeBtn;
                set
                {
                    Model.HomeBtn = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(HomeBtn));
                }
            }

            public string NewsBtn
            {
                get => Model.NewsBtn;
                set
                {
                    Model.NewsBtn = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(NewsBtn));
                }
            }

            public string TotalConfirmedText
            {
                get => Model.TotalConfirmedText;
                set
                {
                    Model.TotalConfirmedText = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalConfirmedText));
                }
            }

            public string TotalDeathsText
            {
                get => Model.TotalDeathsText;
                set
                {
                    Model.TotalDeathsText = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalDeathsText));
                }
            }

            public string TotalRecoveredText
            {
                get => Model.TotalRecoveredText;
                set
                {
                    Model.TotalRecoveredText = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalRecoveredText));
                }
            }

            public string ActiveText
            {
                get => Model.ActiveText;
                set
                {
                    Model.ActiveText = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(ActiveText));
                }
            }

            public string CriticalText
            {
                get => Model.CriticalText;
                set
                {
                    Model.CriticalText = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(CriticalText));
                }
            }

            public string MortalityRateText
            {
                get => Model.MortalityRateText;
                set
                {
                    Model.MortalityRateText = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(MortalityRateText));
                }
            }

            public string WorldwideStatisticsText
            {
                get => Model.WorldwideStatisticsText;
                set
                {
                    Model.WorldwideStatisticsText = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(WorldwideStatisticsText));
                }
            }
        }
    }
}