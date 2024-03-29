﻿using MAV.Base;
using MAV.Client.MVVM.View;
using MAV.Helper;

namespace MAV.Client.MVVM.ViewModel
{
    public class ClientViewModel : ViewModelBase
    {
        //alle Commands um UserControls aufzurufen
        public RelayCommand AddEmployeeViewCommand { get; set; }
        public RelayCommand AddUserViewCommand { get; set; }
        public RelayCommand DirectoryViewCommand { get; set; }
        public RelayCommand HolidayViewCommand { get; set; }
        public RelayCommand ImprintViewCommand { get; set; }
        public RelayCommand SettingsViewCommand { get; set; }
        public RelayCommand EmployeeInfoViewCommand { get; set; }
        public RelayCommand EmployeeEditViewCommand { get; set; }
        
        //Command zum LogOut
        public RelayCommand LogOutCommand { get; private set; }

        //alle Views die aufgerufen werden können
        public AddEmployeeView AddEmployee { get; set; }
        public AddUserView AddUser { get; set; }
        public DirectoryView Directory { get; set; }
        public HolidayView Holiday { get; set; }
        public ImprintView Imprint { get; set; }
        public SettingsView Settings { get; set; }
        public EmployeeInfoView EmployeeInfo { get; set; }
        public EmployeeEditView EmployeeEdit { get; set; }

        private object currentView;
        //Verweis auf aktuell angezeigtes UserControl
        public object CurrentView
        {
            get { return currentView; }
            set
            {
                currentView = value;
                if (currentView == Directory)
                    ((DirectoryViewModel)Directory.DataContext).LoadAddressList(); //neuladen der Adressliste
                OnPropertyChanged();
            }
        }

        private ClientView control;
        //Verweis auf ClientControl
        public ClientView Control
        {
            get { return control; }
            private set {
                if (value != control)
                {
                    control = value;
                    OnPropertyChanged();
                }
            }
        }

        public ClientViewModel(ClientView control, UserModel user) : base(user)
        {
            Control = control;

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
                CurrentView = new AddEmployeeView(User);
            });

            AddUserViewCommand = new RelayCommand(o =>
            {
                CurrentView = new AddUserView(User);
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
                CurrentView = new EmployeeInfoView(o, User, this);
            });

            EmployeeEditViewCommand = new RelayCommand(o =>
            {
                CurrentView = new EmployeeEditView(o, this);
            });

            LogOutCommand = new RelayCommand(LogOut);
        }

        //zurückkehren zum Login
        private void LogOut(object parameter = null)
        {
            Control.Close();
        }
    }
}
