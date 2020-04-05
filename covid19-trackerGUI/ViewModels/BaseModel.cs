using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeuxjeux20.Mvvm;

namespace covid19_trackerGUI.ViewModels
{
    public class BaseModel
    {
        public class Country : ViewModelBase<Base.Country>
        {
            public string Name
            {
                get => Model.name;
                set
                {
                    Model.name = value;
                    OnPropertyChanged();
                    OnPropertyChanged(propertyName: nameof(Name));
                }
            }
            public string NewCases
            {
                get => Model.NewCases;
                set
                {
                    Model.NewCases = value;
                    OnPropertyChanged();
                    OnPropertyChanged(propertyName: nameof(NewCases));
                }
            }
            public int Active
            {
                get => Model.Active;
                set
                {
                    Model.Active = value;
                    OnPropertyChanged();
                    OnPropertyChanged(propertyName: nameof(Active));
                }
            }
            public int Critical
            {
                get => Model.Critical;
                set
                {
                    Model.Critical = value;
                    OnPropertyChanged();
                    OnPropertyChanged(propertyName: nameof(Critical));
                }
            }
            public int Recovered
            {
                get => Model.Recovered;
                set
                {
                    Model.Recovered = value;
                    OnPropertyChanged();
                    OnPropertyChanged(propertyName: nameof(Recovered));
                }
            }
            public int TotalCases
            {
                get => Model.TotalCases;
                set
                {
                    Model.TotalCases = value;
                    OnPropertyChanged();
                    OnPropertyChanged(propertyName: nameof(TotalCases));
                }
            }
            public string NewDeaths
            {
                get => Model.NewDeaths;
                set
                {
                    Model.NewDeaths = value;
                    OnPropertyChanged();
                    OnPropertyChanged(propertyName: nameof(NewDeaths));
                }
            }
            public int TotalDeaths
            {
                get => Model.TotalDeaths;
                set
                {
                    Model.TotalDeaths = value;
                    OnPropertyChanged();
                    OnPropertyChanged(propertyName: nameof(TotalDeaths));

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
                    OnPropertyChanged(propertyName: nameof(wwNewCases));
                }
            }
            public int wwActive
            {
                get => Model.wwActive;
                set
                {
                    Model.wwActive = value;
                    OnPropertyChanged();
                    OnPropertyChanged(propertyName: nameof(wwActive));
                }
            }
            public int wwCritical
            {
                get => Model.wwCritical;
                set
                {
                    Model.wwCritical = value;
                    OnPropertyChanged();
                    OnPropertyChanged(propertyName: nameof(wwCritical));
                }
            }
            public int wwRecovered
            {
                get => Model.wwRecovered;
                set
                {
                    Model.wwRecovered = value;
                    OnPropertyChanged();
                    OnPropertyChanged(propertyName: nameof(wwRecovered));
                }
            }
            public int wwTotalCases
            {
                get => Model.wwTotalCases;
                set
                {
                    Model.wwTotalCases = value;
                    OnPropertyChanged();
                    OnPropertyChanged(propertyName: nameof(wwTotalCases));
                }
            }
            public string wwNewDeaths
            {
                get => Model.wwNewDeaths;
                set
                {
                    Model.wwNewDeaths = value;
                    OnPropertyChanged();
                    OnPropertyChanged(propertyName: nameof(wwNewDeaths));
                }
            }
            public int wwTotalDeaths
            {
                get => Model.wwTotalDeaths;
                set
                {
                    Model.wwTotalDeaths = value;
                    OnPropertyChanged();
                    OnPropertyChanged(propertyName: nameof(wwTotalDeaths));
                }
            }
        }
    }
}
