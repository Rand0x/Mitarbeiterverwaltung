using MAV.Base;
using MAV.Client.MVVM.Model;
using MAV.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace MAV.Client.MVVM.ViewModel
{
    public class EmployeeInfoViewModel : PropertyChangedBase
    {
        private EmployeeModel m_Employee;
        public EmployeeModel Employee
        {
            get { return m_Employee; }
            set {
                if (value != m_Employee)
                {
                    m_Employee = value;
                    OnPropertyChanged();
                }
            }
        }

        public EmployeeInfoViewModel(int key)
        {
            LoadEmployeeData(key);
        }

        private void LoadEmployeeData(int key)
        {
            var param = new ObservableCollection<SqlParameter>();
            param.Add(new SqlParameter("@nKey", key));

            var data = DBProvider.ExecProcedure("sp_LoadEmployeeData", param);

            foreach(DataRow row in data.Rows)
            {
                Employee = new EmployeeModel()
                {
                    FirstName = row["szFirstName"].ToString(),
                    LastName = row["szLastName"].ToString(),
                    EmplyeeNmb = (int)row["nEmployeeNumber"],
                    LandlineNbr = row["szTelephone"].ToString(),
                    EMail = row["szMail"].ToString(),
                    Department = row["szDepartementName"].ToString(),
                    Job = row["szJobName"].ToString(),
                    HireDate = DateTime.Parse(row["dtRecruitDate"].ToString()),
                    Manager = row["szManager"].ToString(),
                    LandlineNmbPrivate = row["szPrivateTelephone"].ToString(),
                    Birthday = DateTime.Parse(row["dtBirthdate"].ToString()),
                    Sex = row["szSex"].ToString()
                };
            }
        }

    }
}
