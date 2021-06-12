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
            string platzHalter = "platzHalter";

            if(Control.PasswordOld.Password != platzHalter)
            {
                SettingsDialog settingsDialog = new SettingsDialog();
                settingsDialog.Title = "Aktuelle Passwort falsch";
                settingsDialog.DialogLine1.Text = "Das eingegebene Passwort entspricht nicht";
                settingsDialog.DialogLine2.Text = "dem aktuellen Passwort!";
                var result = settingsDialog.ShowAsync();
            }
            else if(Control.PasswordNew.Password.Length < 8)
            {
                SettingsDialog settingsDialog = new SettingsDialog();
                settingsDialog.Title = "Passwort zu kurz";
                settingsDialog.DialogLine1.Text = "Das neue Passwort ist zu kurz.";
                settingsDialog.DialogLine2.Text = "Es muss mindestens 8 Zeichen lang sein.";
                var result = settingsDialog.ShowAsync();
            }
            else if(Control.PasswordNew.Password != Control.PasswordNewRepeat.Password)
            {
                // ToDo: Error, das neue Passwort stimmt nicht mit dem wiederholten Passwort überein
                SettingsDialog settingsDialog = new SettingsDialog();
                settingsDialog.Title = "Passwörter nicht identisch";
                settingsDialog.DialogLine1.Text = "Das neue Passwort ist nicht mit dem";
                settingsDialog.DialogLine2.Text = "wiederholten Passwort identisch.";
                var result = settingsDialog.ShowAsync();
            }
            else if(Control.PasswordOld.Password == Control.PasswordNew.Password)
            {
                // ToDo: Error, das neue Passwort darf nicht das alte Passwort sein (Optional)
                SettingsDialog settingsDialog = new SettingsDialog();
                settingsDialog.Title = "Passwörter gleich";
                settingsDialog.DialogLine1.Text = "Das neue Passwort und das alte Passwort";
                settingsDialog.DialogLine2.Text = "dürfen nicht identisch sein!";
                var result = settingsDialog.ShowAsync();
            }
            else
            {
                // ToDo: Passwort in der Datenbank ändern
                // + Erfolgreich Passwort geändert.
                bool erfolgreich = true;

                if(erfolgreich)
                {
                    SettingsDialog settingsDialog = new SettingsDialog();
                    settingsDialog.Title = "Passwort geändert";
                    settingsDialog.DialogLine1.Text = "Das Passwort wurde erfolgreich geändert!";
                    ClearPwdBoxes();
                    var result = settingsDialog.ShowAsync();
                }
                else
                {
                    SettingsDialog settingsDialog = new SettingsDialog();
                    settingsDialog.Title = "Passwort nicht geändert";
                    settingsDialog.DialogLine1.Text = "Passwort ändern ist fehlgeschlagen!";
                    settingsDialog.DialogLine2.Text = "Versuche es erneut.";
                    ClearPwdBoxes();
                    var result = settingsDialog.ShowAsync();
                }
            }
        }

        //Passwortboxen clearen
        void ClearPwdBoxes()
        {
            Control.PasswordOld.Password = "";
            Control.PasswordNewRepeat.Password = "";
            Control.PasswordNew.Password = "";
        }
        #endregion
    }
}
