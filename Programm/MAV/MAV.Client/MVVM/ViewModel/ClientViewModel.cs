using MAV.Base;
using MAV.Client.Core;
using MAV.Client.MVVM.Model;
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

        public AdministrationViewModel AdministrationVM { get; set; }
        public DirectoryViewModel DirectoryVM { get; set; }
        public HolidayViewModel HolidayVM { get; set; }
        public HomeViewModel HomeVM { get; set; }
        public SettingsViewModel SettingsVM { get; set; }
        public EmployeeInfoViewModel EmployeeInfoVM { get; set; }

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
            
            AdministrationVM = new AdministrationViewModel();
            DirectoryVM = new DirectoryViewModel();
            HolidayVM = new HolidayViewModel();
            HomeVM = new HomeViewModel();
            SettingsVM = new SettingsViewModel();
            EmployeeInfoVM = new EmployeeInfoViewModel();

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

            EmployeeInfoViewCommand = new RelayCommand(o =>
            {
                CurrentView = EmployeeInfoVM;
            });
        }
    }
}
