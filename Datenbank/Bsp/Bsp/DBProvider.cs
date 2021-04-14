using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Bsp
{
  public class DBProvider
  {
    public static string GetConnectionString()
    {
      //Sternchen hier mit Daten von Lokalen Server einfügen
      //dbBsp ist hier bei mir der DB Name
      return "Data Source=(local);Initial Catalog=dbBsp;Integrated Security=sspi;User ID=*********;Password=********";
    }

    public static SqlConnection GetConnection()
    {
      return new SqlConnection(GetConnectionString());
    }

    /// <summary>
    /// Führt Prozedur aus und gibt Ergebnis der Prozedur zurück
    /// </summary>
    /// <param name="proc">Name der Prozedur</param>
    /// <param name="param">Parameter die gegebenfalls übergeben werden müssen</param>
    /// <returns></returns>
    public static DataTable ExecProcedure(string proc, ObservableCollection<SqlParameter> param = null)
    {
      var result = new DataTable();

      try
      {
        //neue Verbindung aufbauen
        using (var con = GetConnection())
        {
          var cmd = new SqlCommand(proc, con);
          cmd.CommandType = CommandType.StoredProcedure;

          //Parameter zu Command hinzufügen
          if (param != null)
          {
            foreach (var par in param)
            {
              cmd.Parameters.Add(par);
            }
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
  }
}
