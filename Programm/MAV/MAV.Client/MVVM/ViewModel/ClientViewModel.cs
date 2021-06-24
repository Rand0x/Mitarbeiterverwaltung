using MAV.Base;
using MAV.Client.MVVM.View;
using MAV.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace MAV.Client.MVVM.ViewModel
{
    public class ClientViewModel : ViewModelBase
    {
        public RelayCommand AddEmployeeViewCommand { get; set; }
        public RelayCommand AddUserViewCommand { get; set; }
        public RelayCommand DirectoryViewCommand { get; set; }
        public RelayCommand HolidayViewCommand { get; set; }
        public RelayCommand ImprintViewCommand { get; set; }
        public RelayCommand SettingsViewCommand { get; set; }
        public RelayCommand EmployeeInfoViewCommand { get; set; }
        public RelayCommand EmployeeEditViewCommand { get; set; }
        

        public RelayCommand LogOutCommand { get; private set; }

        public AddEmployeeView AddEmployee { get; set; }
        public AddUserView AddUser { get; set; }
        public DirectoryView Directory { get; set; }
        public HolidayView Holiday { get; set; }
        public ImprintView Imprint { get; set; }
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
                if (_currentView == Directory)
                    ((DirectoryViewModel)Directory.DataContext).LoadAddressList();
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

        private UserModel user;  // ToDo Eventuell eleganter Lösen

        public ClientViewModel(ClientView control, UserModel user) : base(user)
        {
            Control = control;
            this.user = user;

            Directory = new DirectoryView(user, this);
            Holiday = new HolidayView();
            Imprint = new ImprintView();
            Settings = new SettingsView(user);

            CurrentView = Directory;

            CreateCommands();
        }

        private void CreateCommands()
        {
            AddEmployeeViewCommand = new RelayCommand(o =>
            {
                CurrentView = new AddEmployeeView(o);
            });

            AddUserViewCommand = new RelayCommand(o =>
            {
                CurrentView = new AddUserView(user);
            });

            DirectoryViewCommand = new RelayCommand(o =>
            {
                CurrentView = Directory;
            });

            HolidayViewCommand = new RelayCommand(o =>
            {
                CurrentView = Holiday;
            });

            ImprintViewCommand = new RelayCommand(o =>
            {
                CurrentView = Imprint;
            });

            SettingsViewCommand = new RelayCommand(o =>
            {
                CurrentView = Settings;
            });

            EmployeeInfoViewCommand = new RelayCommand(o =>
            {
                CurrentView = new EmployeeInfoView(o, user);
            });

            EmployeeEditViewCommand = new RelayCommand(o =>
            {
                CurrentView = new EmployeeEditView(o);
            });

            LogOutCommand = new RelayCommand(LogOut);
        }

        private void LogOut(object parameter = null)
        {
            Control.Close();
        }
    }
}
