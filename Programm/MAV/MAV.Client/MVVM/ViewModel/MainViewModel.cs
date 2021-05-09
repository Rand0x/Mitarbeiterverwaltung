using MAV.Base;
using MAV.Client.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAV.Client.MVVM.ViewModel
{
    class MainViewModel : PropertyChangedBase
    {
        public RelayCommand AdministrationViewCommand { get; set; }
        public RelayCommand DirectoryViewCommand { get; set; }
        public RelayCommand HolidayViewCommand { get; set; }
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand SettingsViewCommand { get; set; }

        public AdministrationViewModel AdministrationVM { get; set; }
        public DirectoryViewModel DirectoryVM { get; set; }
        public HolidayViewModel HolidayVM { get; set; }
        public HomeViewModel HomeVM { get; set; }
        public SettingsViewModel SettingsVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }


        public MainViewModel()
        {
            AdministrationVM = new AdministrationViewModel();
            DirectoryVM = new DirectoryViewModel();
            HolidayVM = new HolidayViewModel();
            HomeVM = new HomeViewModel();
            SettingsVM = new SettingsViewModel();

            CurrentView = HomeVM;

            AdministrationViewCommand = new RelayCommand(o =>
            {
                CurrentView = AdministrationVM;
            });

            DirectoryViewCommand = new RelayCommand(o =>
            {
                CurrentView = DirectoryVM;
            });

            HolidayViewCommand = new RelayCommand(o =>
            {
                CurrentView = HolidayVM;
            });

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });

            SettingsViewCommand = new RelayCommand(o =>
            {
                CurrentView = SettingsVM;
            });
        }
    }
}
