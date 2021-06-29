using MAV.Base;
using MAV.Client;
using MAV.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace MAV.Login
{
    public class LoginViewModel : PropertyChangedBase
    {
        #region Properties

        //Verweis auf Frontend um Password nciht zwischenspeichern zu müssen
        private Login m_Control;
        public Login Control
        {
            get { return m_Control; }
            private set {
                if(value != m_Control)
                {
                    m_Control = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Constructor

        public LoginViewModel(Login control)
        {
            Control = control;
            CreateCommands();
        }

        #endregion

        #region Commads

        public RelayCommand LogInCommand { get; private set; }

        /// <summary>
        /// Initialisieren der Commands
        /// </summary>
        private void CreateCommands()
        {
            LogInCommand = new RelayCommand(Login);
        }

        /// <summary>
        /// Logik zum Einlogen und gegebenenfals öffnen mit Hilfe der DB
        /// </summary>
        private void Login(object parameter = null)
        {
            //wenn kein Username angegeben ist wird gar nicht erst ein Anmeldeversuch gestartet
            if (Control.UsernameBox.Text.Length < 1) //Hinweis das kein Passwort eingegeben worden ist
            {
                ShowErrorDialog("Kein Benutzername", "Es wurde kein Benutzername eingegeben.");
                return;
            }
            if(Control.PasswordBox.Password.Length < 1) //Hinweis das kein Passwort eingegeben worden ist
            {
                ShowErrorDialog("Kein Passwort", "Es wurde kein Passwort eingegeben.");
                return;
            }

            var param = new ObservableCollection<SqlParameter>();
            DataTable result;

            //Übergabeparameter für die Prozedur
            param.Add(new SqlParameter("@szUserName", Control.UsernameBox.Text));

            try
            {
                result = DBProvider.ExecProcedure("sp_LogIn", param); //Prozedur ausführen

                if (result.Rows.Count > 0) //Anmeldung war erfolgreich
                {
                    var pwd = result.Rows[0]["szPassword"].ToString();
                    var salt = ByteStringConverter.ToByteFromString(result.Rows[0]["szSalt"].ToString());

                    if (pwd == ByteStringConverter.ToStringFromBytes(PasswordHash.Hash(Control.PasswordBox.Password, salt)))
                    {
                        //erstellen neus UserModel dass ID des Users und dessen Recht beinhalted
                        var user = new UserModel(
                            (int)result.Rows[0]["nKey"], //nKey des Users
                            result.Rows[0]["nEmployeeLink"] is DBNull ? null : (int?)result.Rows[0]["nEmployeeLink"], //nKey des Mitarbeiters
                            (int)result.Rows[0]["nRightLink"], //nKey des Rechts
                            result.Rows[0]["szRightName"].ToString() //Name des Rechts
                        );

                        //Client starten und Login schließen
                        var client = new ClientView(user);
                        Control.Hide();
                        client.ShowDialog();

                        try //wenn Anwendung über X geschlossen wird kommt Fehler und beendigung des Programms, ansonsten kann sich wieder eingelogt werden
                        {
                            Control.Show();
                        }
                        catch (Exception ex)
                        {
                            return;
                        }
                    }
                    else
                        ShowErrorDialog("Anmeldung fehlgeschlagen", "Benutzername oder Passwort falsch!", "Prüfen Sie Ihre Eingaben.");
                }
                else //Fehler beim Anmeldeversuch:                
                    ShowErrorDialog("Anmeldung fehlgeschlagen", "Benutzername oder Passwort falsch!", "Prüfen Sie Ihre Eingaben.");                
            }
            catch(Exception ex)
            {
                ShowErrorDialog("Fehler", ex.Message);
            }

            //Eingaben clearen und Fokus auf UsernameBox setzen
            Control.UsernameBox.Clear();
            Control.PasswordBox.Clear();
            Control.UsernameBox.Focus();
        }

        #endregion

        #region Dialog

        private void ShowErrorDialog(string title, string firstLine, string secondLine = null)
        {
            Dialog dialog = new Dialog
            {
                Title = title,
                FirstLineContent = firstLine,
                SecondLineText = secondLine
            };
            var resultDialog = dialog.ShowAsync();
        }

        #endregion
    }
}
