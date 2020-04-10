using System;
using Jeuxjeux20.Mvvm;

namespace covid19_tracker.ViewModels
{
    public class BaseModel : ViewModelBase<Base>
    {
        public DateTime UpDateTime
        {
            get => Model.UpDateTime;
            set
            {
                Model.UpDateTime = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(UpDateTime));
            }
        }

        public int nextUpdateIn
        {
            get => Model.nextUpdateIn;
            set
            {
                Model.nextUpdateIn = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(nextUpdateIn));
            }
        }

        public class Country : ViewModelBase<Base.Country>
        {
            public string Name
            {
                get => Model.name;
                set
                {
                    Model.name = value;
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
        }


        public class Worldwide : ViewModelBase<Base.Worldwide>
        {
            public string wwNewCases
            {
                get => Model.wwNewCases;
                set
                {
                    Model.wwNewCases = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(wwNewCases));
                }
            }

            public int wwActive
            {
                get => Model.wwActive;
                set
                {
                    Model.wwActive = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(wwActive));
                }
            }

            public int wwCritical
            {
                get => Model.wwCritical;
                set
                {
                    Model.wwCritical = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(wwCritical));
                }
            }

            public int wwRecovered
            {
                get => Model.wwRecovered;
                set
                {
                    Model.wwRecovered = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(wwRecovered));
                }
            }

            public int wwTotalCases
            {
                get => Model.wwTotalCases;
                set
                {
                    Model.wwTotalCases = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(wwTotalCases));
                }
            }

            public string wwNewDeaths
            {
                get => Model.wwNewDeaths;
                set
                {
                    Model.wwNewDeaths = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(wwNewDeaths));
                }
            }

            public int wwTotalDeaths
            {
                get => Model.wwTotalDeaths;
                set
                {
                    Model.wwTotalDeaths = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(wwTotalDeaths));
                }
            }
        }
    }
}