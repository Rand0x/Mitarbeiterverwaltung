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
        public RelayCommand ChangePwdCommand { get; private set; }

        /// <summary>
        /// Initialisieren der Commands
        /// </summary>
        private void CreateCommands()
        {
            LogInCommand = new RelayCommand(Login);
            ChangePwdCommand = new RelayCommand(ChangePwd);
        }

        //ToDo wenn DB Provider vorhanden ist, auf diesen umstellen
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

            //Übergabeparameter für die Prozedur
            param.Add(new SqlParameter("@szUserName", Control.UsernameBox.Text));
            //param.Add(new SqlParameter("@szPassword", Control.PasswordBox.Password));

            var result = tmp_ExecProc("sp_LogIn", param); //Prozedur ausführen

            if (result != null) // Prüfen ob Verbindung mit Server erfolgreich war.
            {
                if (result.Rows.Count > 0) //Anmeldung war erfolgreich
                {
                    var pwd = result.Rows[0]["szPassword"].ToString();
                    var salt = ByteStringConverter.ToByteFromString(result.Rows[0]["szSalt"].ToString());

                    if (pwd == ByteStringConverter.ToStringFromBytes(PasswordHash.Hash(Control.PasswordBox.Password, salt)))
                    {
                        //erstellen neus UserModel dass ID des Users und dessen Recht beinhalted
                        var user = new UserModel(
                          result.Rows[0]["nEmployeeLink"] is DBNull ? null : (int?)result.Rows[0]["nEmployeeLink"], //nKey des Users
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
                }
                else //Fehler beim Anmeldeversuch:
                {
                    ShowErrorDialog("Anmeldung fehlgeschlagen", "Benutzername oder Passwort falsch!", "Prüfen Sie Ihre Eingaben.");
                    return;
                }
            }
            else  // Keine Verbindung zum Server
            {
                ShowErrorDialog("Anmeldung fehlgeschlagen", "Es konnte keine Verbindung zum Server hergestellt werden.", "Prüfen Sie Ihre VPN-Verbindung!");
                return;
            }            

            //Eingaben clearen und Fokus auf UsernameBox setzen
            Control.UsernameBox.Clear();
            Control.PasswordBox.Clear();
            Control.UsernameBox.Focus();
        }

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

        private void ChangePwd(object parameter = null)
        {
            //ToDo Implementiern
            // --
            // Chris: Das "Passwort Vergessen" habe ich entfernt. Das können wir noch
            // hinzufügen wenn wir Zeit dafür haben!
            // --
        }

        #endregion

        //ToDo entfernen
        //nur temporär bis DBProvider existiert
        #region tmp_Helper

        private string conStr = "Data Source=141.75.150.78,49724\\MAVSQL01;Initial Catalog=dbMAV;User ID=sa;Password=MAVAdmin01";

        private DataTable tmp_ExecProc(string proc, ObservableCollection<SqlParameter> param = null)
        {
            var result = new DataTable();

            try
            {
                //neue Verbindung aufbauen
                using (var con = new SqlConnection(conStr))
                {
                    var cmd = new SqlCommand(proc, con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Parameter zu Prozedur hinzufügen
                    foreach (var par in param)
                    {
                        cmd.Parameters.Add(par);
                    }

                    con.Open();

                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        //füllen des ERgebnisses der Prozedur in result
                        adapter.Fill(result);
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                //...
                result = null;
            }
            return result;
        }

        #endregion

    }
}
