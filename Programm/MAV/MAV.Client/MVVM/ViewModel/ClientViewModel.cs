using MAV.Base;
using MAV.Client.MVVM.View;
using MAV.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace MAV.Client.MVVM.ViewModel
{
    class ClientViewModel : ViewModelBase
    {
        public RelayCommand AdministrationViewCommand { get; private set; }
        public RelayCommand DirectoryViewCommand { get; private set; }
        public RelayCommand HolidayViewCommand { get; private set; }
        public RelayCommand HomeViewCommand { get; private set; }
        public RelayCommand SettingsViewCommand { get; private set; }
        public RelayCommand EmployeeInfoViewCommand { get; private set; }

        public RelayCommand LogOutCommand { get; private set; }

        public AdministrationView Administration { get; private set; }
        public DirectoryView Directory { get; private set; }
        public HolidayView Holiday { get; private set; }
        public HomeView Home { get; private set; }
        public SettingsView Settings { get; private set; }
        public EmployeeInfoView EmployeeInfo { get; private set; }

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

        private ClientView m_Control;
        public ClientView Control
        {
            get { return m_Control; }
            private set {
                if (value != m_Control)
                {
                    m_Control = value;
                    OnPropertyChanged();
                }
            }
        }


        public ClientViewModel(ClientView control, UserModel user) : base(user)
        {
            Control = control;

            Administration = new AdministrationView();
            Directory = new DirectoryView();
            Holiday = new HolidayView();
            Home = new HomeView();
            Settings = new SettingsView();
            EmployeeInfo = new EmployeeInfoView();
            
            CurrentView = Home;

            CreateCommands();
        }

        private void CreateCommands()
        {
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

            LogOutCommand = new RelayCommand(LogOut);
        }

        private void LogOut(object parameter = null)
        {
            Control.Close();
        }
    }
}
