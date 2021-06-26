using System;
using System.Collections.Generic;
using System.Text;
using MAV.Helper;
using MAV.Base;
using MAV.Client.MVVM.View;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;

namespace MAV.Client.MVVM.ViewModel
{
    class SettingsViewModel : ViewModelBase
    {
        #region Properties

        private SettingsView m_Control;
        //Verweis auf View
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

        public SettingsViewModel(UserModel user, SettingsView control) : base(user)
        {
            Control = control;
            CreateCommands();
        }

        #endregion

        #region Commands

        //Command um Passwort zu ändern
        public RelayCommand ChangePwdCommand { get; private set; }

        /// <summary>
        /// Zusweisen von Methoden zu den Commands
        /// </summary>
        private void CreateCommands()
        {
            ChangePwdCommand = new RelayCommand(ChangePassword);
        }

        /// <summary>
        /// Ändert das Passwort des aktuellen Benutzers
        /// </summary>
        /// <param name="parameter"></param>
        private void ChangePassword(object parameter = null)
        {
            //laden des alten Passworts und Salts aus der DB
            var param = new ObservableCollection<SqlParameter>();
            param.Add(new SqlParameter("@nKey", UserKey));
            DataTable result;

            try
            {
                result = DBProvider.ExecProcedure("sp_LoadPassword", param);
            }
            catch(Exception ex)
            {
                DialogPopUp("Fehler", ex.Message);
                ClearPwdBoxes();
                return;
            }

            //ermitteln des alten Passworts/Salts
            var pwdOld = result.Rows[0]["szPassword"].ToString();
            var saltOld = ByteStringConverter.ToByteFromString(result.Rows[0]["szSalt"].ToString());

            if (ByteStringConverter.ToStringFromBytes(PasswordHash.Hash(Control.PasswordOld.Password, saltOld)) != pwdOld) //Überprüfen des alten Passworts
            {
                DialogPopUp("Aktuelles Passwort falsch", "Das eingegebene Passwort entspricht nicht", "dem aktuellen Passwort");
            }
            else if (Control.PasswordNew.Password.Length < 8) //neues Passwort lang genug?
            {
                DialogPopUp("Passwort zu kurz", "Das neue Passwort ist zu kurz.", "Es muss mindestens acht Zeichen lang sein.");
            }
            else if (Control.PasswordNew.Password != Control.PasswordNewRepeat.Password) //neue Passwörter stimmen über ein?
            {
                DialogPopUp("Passwort nicht identisch", "Das neue Passwort ist nicht dem", "wiederholten Passwort identisch.");
            }
            else if (Control.PasswordOld.Password == Control.PasswordNew.Password) //altes und neues Passwort sind unterschiedlich?
            {
                DialogPopUp("Passwörter sind gleich", "Das neue Passwort und das alte Passwort", "dürfen nicht identisch sein!");
            }
            else //altes Passwort korrekt und alle anderen Anforderungen erfüllt
            {
                //neues Passwort hashen und Salt ermitteln
                var pwdNew = PasswordHash.Hash(Control.PasswordNew.Password, out var saltNew);

                //speichern des neuen Passworts und Salts in DB
                var paramNew = new ObservableCollection<SqlParameter>();
                paramNew.Add(new SqlParameter("@nKey", UserKey));
                paramNew.Add(new SqlParameter("@szPassword", ByteStringConverter.ToStringFromBytes(pwdNew)));
                paramNew.Add(new SqlParameter("@szSalt", ByteStringConverter.ToStringFromBytes(saltNew)));

                try
                {
                    DBProvider.ExecProcedure("sp_ReplacePassword", paramNew);
                }
                catch(Exception ex)
                {
                    DialogPopUp("Fehler", ex.Message);
                    ClearPwdBoxes();
                    return;
                }

                DialogPopUp("Passwort geändert", "Das Passwort wurde erfolgreich geändert!");
            }

            ClearPwdBoxes();
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

        #region Dialog

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

        #endregion
    }
}
