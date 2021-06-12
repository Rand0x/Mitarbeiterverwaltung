using System;
using System.Collections.Generic;
using System.Text;
using MAV.Helper;
using MAV.Base;
using MAV.Client.MVVM.View;

namespace MAV.Client.MVVM.ViewModel
{
    class SettingsViewModel : PropertyChangedBase
    {
        #region Properties
        private SettingsView m_Control;

        public SettingsView Control
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

        public SettingsViewModel (SettingsView control)
        {
            Control = control;
            CreateCommands();
        }

        #endregion

        #region Commands

        public RelayCommand ChangePwdCommand { get; private set; }

        private void CreateCommands()
        {
            ChangePwdCommand = new RelayCommand(ChangePassword);

        }

        private void ChangePassword(object parameter=null)
        {
            // ToDo: Passwort herholen von der Datenbank
            string platzHalter = "";

            if(Control.PasswordOld.Password != platzHalter)
            {
                // ToDo: Error, aktuelles Passwort falsch
            }
            else if(Control.PasswordNew.Password.Length < 8)
            {
                // ToDo: Error, Passwort zu kurz
            }
            else if(Control.PasswordNew.Password != Control.PasswordNewRepeat.Password)
            {
                // ToDo: Error, das neue Passwort stimmt nicht mit dem wiederholten Passwort überein
            }
            else if(Control.PasswordOld.Password == Control.PasswordNew.Password)
            {
                // ToDo: Error, das neue Passwort darf nicht das alte Passwort sein (Optional)
            }
            else
            {
                // ToDo: Passwort in der Datenbank ändern
                // + Erfolgreich Passwort geändert.
            }
        }
        #endregion
    }
}
