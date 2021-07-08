using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;


namespace MAV.Helper
{
    public class DBProvider
    {
        /// <summary>
        /// gibt Verbindungsstring für unseren SQL-Server zurück
        /// </summary>
        /// <returns></returns>
        public static string GetConnectionString()
        {
            return "Data Source=141.75.150.78,49724\\MAVSQL01;Initial Catalog=dbMAV;User ID=sa;Password=MAVAdmin01";
        }

        /// <summary>
        /// Liefert Connection zum SQL-Server
        /// </summary>
        /// <returns></returns>
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(GetConnectionString());
        }

        /// <summary>
        /// Führt Prozedur auf SQL-Server aus
        /// </summary>
        /// <param name="proc">Prozedurname</param>
        /// <param name="param">(optionale) Übergabeparameter</param>
        /// <returns></returns>
        public static DataTable ExecProcedure(string proc, ObservableCollection<SqlParameter> param = null)
        {
            var result = new DataTable();

            try
            {
                //Verbindung zum Server aufbauen
                using (var con = GetConnection())
                {
                    //Prozedur angeben
                    var cmd = new SqlCommand(proc, con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //falls vorhanden Parameter übergeben
                    if (param != null)
                    {
                        foreach (var par in param)
                        {
                            cmd.Parameters.Add(par);
                        }
                    }

                    //Verbindung öffnen
                    con.Open();

                    //Result aus Datenbank abspeichern
                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(result);
                    }

                    //Verbindung schließen
                    con.Close();
                }
            }
            catch (Exception ex) //Fehler aus Datenbank abfangen und weiterleiten
            {
                throw ex;
            }

            //Ergebnis aus Datenbank zurückgeben 
            return result;
        }
    }
}
