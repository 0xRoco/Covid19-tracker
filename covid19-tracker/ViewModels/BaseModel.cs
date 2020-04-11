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

            public double DeathRate
            {
                get => Model.deathRate;
                set
                {
                    Model.deathRate = value;
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
    }
}