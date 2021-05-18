using MAV.Base;
using MAV.Client.Core;
using MAV.Client.MVVM.Model;
using MAV.Client.MVVM.View;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAV.Client.MVVM.ViewModel
{
    class ClientViewModel : PropertyChangedBase
    {
        public RelayCommand AdministrationViewCommand { get; set; }
        public RelayCommand DirectoryViewCommand { get; set; }
        public RelayCommand HolidayViewCommand { get; set; }
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand SettingsViewCommand { get; set; }
        public RelayCommand EmployeeInfoViewCommand { get; set; }


        public AdministrationView Administration { get; set; }
        public DirectoryView Directory { get; set; }
        public HolidayView Holiday { get; set; }
        public HomeView Home { get; set; }
        public SettingsView Settings { get; set; }
        public EmployeeInfoView EmployeeInfo { get; set; }

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

        private UserModel m_User;
        public UserModel User
        {
            get { return m_User; }
            private set {
                if (value != m_User)
                {
                    m_User = value;
                    OnPropertyChanged();
                }
            }
        }

        public ClientViewModel(UserModel user)
        {
            User = user;

            Administration = new AdministrationView();
            Directory = new DirectoryView();
            Holiday = new HolidayView();
            Home = new HomeView();
            Settings = new SettingsView();
            EmployeeInfo = new EmployeeInfoView();

            CurrentView = Home;

            AdministrationViewCommand = new RelayCommand(o =>
            {
                CurrentView = Administration;
            });

            DirectoryViewCommand = new RelayCommand(o =>
            {
                CurrentView = Directory;
            });

            HolidayViewCommand = new RelayCommand(o =>
            {
                CurrentView = Holiday;
            });

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = Home;
            });

            SettingsViewCommand = new RelayCommand(o =>
            {
                CurrentView = Settings;
            });

            EmployeeInfoViewCommand = new RelayCommand(o =>
            {
                CurrentView = EmployeeInfo;
            });
        }
    }
}
