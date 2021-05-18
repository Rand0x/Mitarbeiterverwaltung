using MAV.Base;
using MAV.Client;
using MAV.Client.MVVM.Model;
using MAV.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows;

namespace MAV.Login
{
    public class LoginViewModel : PropertyChangedBase
    {

        #region Variables

        //Verweis auf Frontend um Password nciht zwischenspeichern zu müssen
        private Login m_Control;

        #endregion

        #region Constructor

        public LoginViewModel(Login control)
        {
            m_Control = control;
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

        /// <summary>
        /// Logik zum Einlogen und gegebenenfals öffnen mit Hilfe der DB
        /// </summary>
        private void Login(object parameter = null)
        {
            //wenn kein Username angegeben ist wird gar nicht erst ein Anmeldeversuch gestartet
            if (m_Control.UsernameBox.Text is null)
                return;

            var param = new ObservableCollection<SqlParameter>();

            //Übergabeparameter für die Prozedur
            param.Add(new SqlParameter("@szUserName", m_Control.UsernameBox.Text));
            param.Add(new SqlParameter("@szPassword", m_Control.PasswordBox.Password));

            var result = tmp_ExecProc("sp_LogIn", param); //Prozedur ausführen

            if (result.Rows.Count > 0) //Anmeldung war erfolgreich
            {
                //erstellen neus UserModel dass ID des Users und dessen Recht beinhalted
                var user = new UserModel(
                  result.Rows[0]["nEmployeeLink"] is DBNull ? null : (int?)result.Rows[0]["nEmployeeLink"], //nKey des Users
                  (int)result.Rows[0]["nRightLink"], //nKey des Rechts
                  result.Rows[0]["szName"].ToString() //Name des Rechts
                );

                //Client starten und Login schließen
                m_Control.Close();
                var client = new ClientView(user);
                client.ShowDialog();
            }
            else //Anmaeldung nicht erfolgreich
            {
                m_Control.UsernameBox.Clear();
                m_Control.PasswordBox.Clear();
                //ToDo Fehlertext aus DB anzeigen lassen auf Frontend
            }
        }

        private void ChangePwd(object parameter = null)
        {
            //ToDo Implementiern
        }

        #endregion

        //ToDo entfernen
        //nur temporär bis DBProvider existiert
        #region tmp_Helper

        private string conStr = "Data Source=(local);Initial Catalog=dbMAV;Integrated Security=sspi;User ID=DESKTOP-RL90RDQ\\Tobias;Password=091216";

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
