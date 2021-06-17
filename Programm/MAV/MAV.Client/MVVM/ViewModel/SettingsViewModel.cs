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

        public SettingsViewModel(SettingsView control)
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

        private void ChangePassword(object parameter = null)
        {
            // ToDo: Passwort herholen von der Datenbank
            string platzHalter = "platzHalter";

            if (Control.PasswordOld.Password != platzHalter)
            {
                DialogPopUp("Aktuelle Passwort falsch", "Das eingegebene Passwort entspricht nicht", "dem aktuellen Passwort");
            }
            else if (Control.PasswordNew.Password.Length < 8)
            {
                DialogPopUp("Passwort zu kurz", "Das neue Passwort ist zu kurz.", "Es muss mindestens acht Zeichen lang sein.");
            }
            else if (Control.PasswordNew.Password != Control.PasswordNewRepeat.Password)
            {
                DialogPopUp("Passwort nicht identisch", "Das neue Passwort ist nicht dem", "wiederholten Passwort identisch.");
            }
            else if (Control.PasswordOld.Password == Control.PasswordNew.Password)
            {
                DialogPopUp("Passwörter sind gleich", "Das neue Passwort und das alte Passwort", "dürfen nicht identisch sein!");
            }
            else
            {
                // ToDo: Passwort in der Datenbank ändern
                // + Erfolgreich Passwort geändert.
                bool erfolgreich = true;

                if (erfolgreich)
                {
                    DialogPopUp("Passwort geändert", "Das Passwort wurde erfolgreich geändert!");
                    ClearPwdBoxes();
                }
                else
                {
                    DialogPopUp("Passwort nicht geändert!", "Das Passwort ändern ist fehlgeschlagen!", "Versuche es erneunt.");
                    ClearPwdBoxes();
                }
            }
        }

        /// <summary>
        /// Beim Aufrufen erscheint ein Fenster
        /// </summary>
        /// <param name="title">Title des Fensters</param>
        /// <param name="firstLine">Erste Zeile des Fensters</param>
        /// <param name="secondLine">Zweite Zeile des Fensters</param>
        private void DialogPopUp(string title, string firstLine, string secondLine = "")
        {
            Dialog dialog = new Dialog();
            dialog.Title = title;
            dialog.FirstLineContent = firstLine;
            dialog.SecondLineText = secondLine;
            var result = dialog.ShowAsync();
        }



        /// <summary>
        /// Leert die Passwortboxen
        /// </summary>
        void ClearPwdBoxes()
        {
            Control.PasswordOld.Password = "";
            Control.PasswordNewRepeat.Password = "";
            Control.PasswordNew.Password = "";
        }
        #endregion
    }
}
