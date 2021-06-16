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
        public RelayCommand AddUserViewCommand { get; set; }
        public RelayCommand DirectoryViewCommand { get; set; }
        public RelayCommand HolidayViewCommand { get; set; }
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand SettingsViewCommand { get; set; }
        public RelayCommand EmployeeInfoViewCommand { get; set; }
        public RelayCommand EmployeeEditViewCommand { get; set; }

        public RelayCommand LogOutCommand { get; private set; }

        public AddUserView AddUser { get; set; }
        public DirectoryView Directory { get; set; }
        public HolidayView Holiday { get; set; }
        public HomeView Home { get; set; }
        public SettingsView Settings { get; set; }
        public EmployeeInfoView EmployeeInfo { get; set; }
        public EmployeeEditView EmployeeEdit { get; set; }

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

            AddUser = new AddUserView(user);
            Directory = new DirectoryView();
            Holiday = new HolidayView();
            Home = new HomeView();
            Settings = new SettingsView();

            CurrentView = Home;

            CreateCommands();
        }

        private void CreateCommands()
        {
            AddUserViewCommand = new RelayCommand(o =>
            {
                CurrentView = AddUser;
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
                CurrentView = new EmployeeInfoView();
            });

            EmployeeEditViewCommand = new RelayCommand(o =>
            {
                CurrentView = new EmployeeEditView();
            });

            LogOutCommand = new RelayCommand(LogOut);
        }

        private void LogOut(object parameter = null)
        {
            Control.Close();
        }
    }
}
