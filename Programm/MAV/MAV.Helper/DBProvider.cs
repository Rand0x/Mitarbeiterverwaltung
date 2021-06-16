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
        public static string GetConnectionString()
        {
            return "Data Source=141.75.150.78,49724\\MAVSQL01;Initial Catalog=dbMAV;User ID=sa;Password=MAVAdmin01";
        }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(GetConnectionString());
        }

        public static DataTable ExecProcedure(string proc, ObservableCollection<SqlParameter> param = null)
        {
            var result = new DataTable();

            try
            {
                using (var con = GetConnection())
                {
                    var cmd = new SqlCommand(proc, con);
                    cmd.CommandType = CommandType.StoredProcedure;

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
                        adapter.Fill(result);
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }
    }
}
