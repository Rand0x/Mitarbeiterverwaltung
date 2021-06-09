using System;
using System.Collections.Generic;
using System.Text;
using MAV.Helper;
using MAV.Base;
using MAV.Client.MVVM.View;

namespace MAV.Client.MVVM.ViewModel
{
    class AddUserViewModel : PropertyChangedBase
    {
        #region Properties
        private AddUserView m_Control;
        public AddUserView Control
        {
            get { return m_Control; }
            private set
            {
                if (value != m_Control)
                {
                    m_Control = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Constructor

        public AddUserViewModel(AddUserView control)
        {
            Control = control;
            CreateCommands();
        }

        #endregion


        #region Commands

        public RelayCommand AddUserCommand { get; private set; }

        private void CreateCommands()
        {
            AddUserCommand = new RelayCommand(AddUser);
        }

        #endregion



        private void AddUser(object parameter = null)
        {
            if(Control.LastName.Text is null)
            {
                //ToDo: Meldung, dass Name leer ist
                
            }
            else if(Control.FirstName.Text is null)
            {
                //ToDo: Meldung, dass Vorname leer ist
            }
            else if(Control.Password.Password is null)
            {
                //ToDo: Meldung, dass kein Passwort gesetzt worden ist
            }
            else if (Control.Password.Password != Control.PasswordRepeat.Password)
            {
                //ToDo: Meldung Passwort und das Passwort zum Wiederholen ist nicht identisch
            }
            else
            {
                //ToDo: Datenbankverbindung um den User zu erstellen!
            }

        }

    }
}
