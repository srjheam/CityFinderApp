using CityFinder.Helpers;
using CityFinder.Models;
using CityFinder.Services;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace CityFinder.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        bool _doesZipCodeExist;
        bool _isQueryEmpty;
        bool _isQueryInvalid;
        string _searchQuery;
        NotifyTaskCompletion<ZipCodeInfo> _zipCodeInfo;


        public MainPageViewModel()
        {
            SearchQuery = String.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool DoesZipCodeExist
        {
            get => _doesZipCodeExist;
            set
            {
                if (_doesZipCodeExist != value)
                {
                    _doesZipCodeExist = value;
                    OnPropertyChanged(nameof(DoesZipCodeExist));
                }
            }
        }

        public bool IsQueryEmpty
        {
            get => _isQueryEmpty;
            set
            {
                if (_isQueryEmpty != value)
                {
                    _isQueryEmpty = value;
                    OnPropertyChanged(nameof(IsQueryEmpty));
                }
            }
        }

        public bool IsQueryInvalid
        {
            get => _isQueryInvalid;
            set
            {
                if (_isQueryInvalid != value)
                {
                    _isQueryInvalid = value;
                    OnPropertyChanged(nameof(IsQueryInvalid));
                }
            }
        }

        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                DoesZipCodeExist = false;
                if (_searchQuery != value)
                {
                    _searchQuery = value;
                    IsQueryEmpty = String.IsNullOrWhiteSpace(_searchQuery);
                    if (ZipCode.IsZipCodeValid(_searchQuery))
                    {
                        IsQueryInvalid = false;
                        ZipCodeInfo = new NotifyTaskCompletion<ZipCodeInfo>(SetZipCodeInfo());
                    }
                    else
                    {
                        IsQueryInvalid = !_isQueryEmpty;
                    }
                    OnPropertyChanged(nameof(SearchQuery));
                }
            }
        }

        public NotifyTaskCompletion<ZipCodeInfo> ZipCodeInfo
        {
            get => _zipCodeInfo;
            set
            {
                if (_zipCodeInfo != value)
                {
                    _zipCodeInfo = value;
                    OnPropertyChanged(nameof(ZipCodeInfo));
                }
            }
        }

        public async Task<ZipCodeInfo> SetZipCodeInfo()
        {
            TimeSpan BASE_ELAPSED_TIME = new TimeSpan(0, 0, 1);
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            bool wasInvalid;
            ZipCodeInfo zipCodeInfo = null;
            if (!ZipCode.TryParseZipCode(_searchQuery, out ZipCode zipCode))
            {
                DoesZipCodeExist = false;
                wasInvalid = true;
            }
            else
            {
                try
                {
                    zipCodeInfo = await ZipCodeInfoService.GetAsync(zipCode.CountryAbbreviation, zipCode.PostCode);
                    DoesZipCodeExist = true;
                    wasInvalid = false;
                }
                catch (Exception)
                {
                    DoesZipCodeExist = false;
                    wasInvalid = true;
                }
            }

            stopwatch.Stop();
            if (BASE_ELAPSED_TIME > stopwatch.Elapsed)
            {
                var napDuration = BASE_ELAPSED_TIME - stopwatch.Elapsed;
                System.Threading.Thread.Sleep(napDuration);
            }

            IsQueryInvalid = wasInvalid;
            return zipCodeInfo;
        }

        protected virtual void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
