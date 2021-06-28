using MAV.Base;
using MAV.Client.MVVM.Model;
using MAV.Client.MVVM.View;
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
        #region Properties

        private EmployeeModel m_Employee;
        //ausgewählter Mitarbeiter
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

        private ClientViewModel m_ClientVM;
        public ClientViewModel ClientVM
        {
            get { return m_ClientVM; }
            set
            {
                if (value != m_ClientVM)
                {
                    m_ClientVM = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Constructor

        public EmployeeInfoViewModel(int key, ClientViewModel clientVM)
        {
            ClientVM = clientVM;
            LoadEmployeeData(key);
            CreateCommands();
        }

        #endregion

        #region Commands

        public RelayCommand EditCommand { get; private set; }

        private void CreateCommands()
        {
            EditCommand = new RelayCommand((object o) => 
            {
                ClientVM.EmployeeEditViewCommand.Execute(Employee.Key);
            });
        }

        #endregion

        #region Implementation

        /// <summary>
        /// Laden der Daten des ausgewählten Mitarbeiters
        /// </summary>
        /// <param name="key"></param>
        private void LoadEmployeeData(int key)
        {
            DataTable data;
            var param = new ObservableCollection<SqlParameter>();
            param.Add(new SqlParameter("@nKey", key));

            try
            {
                data = DBProvider.ExecProcedure("sp_LoadEmployeeData", param);
            }
            catch(Exception ex)
            {
                DialogPopUp("Fehler", ex.Message);
                return;
            }

            //Mitarbeiter hinzufügen
            foreach(DataRow row in data.Rows)
            {
                Employee = new EmployeeModel()
                {
                    Key = (int)row["nKey"],
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

        /// <summary>
        /// Beim Aufrufen erscheint ein Fenster
        /// </summary>
        /// <param name="title">Title des Fensters</param>
        /// <param name="firstLine">Erste Zeile des Fensters</param>
        /// <param name="secondLine">Zweite Zeile des Fensters</param>
        protected void DialogPopUp(string title, string firstLine, string secondLine = "")
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
