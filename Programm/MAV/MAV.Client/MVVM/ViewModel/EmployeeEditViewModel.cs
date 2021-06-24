﻿using MAV.Base;
using MAV.Client.MVVM.Model;
using MAV.Client.MVVM.View;
using MAV.DirectoryModule.Model;
using MAV.Helper;
using ModernWpf.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace MAV.Client.MVVM.ViewModel
{
    public class EmployeeEditViewModel : EmployeeInfoViewModel
    {

        #region Properties

        private ObservableCollection<DepartmentModel> m_Departements;
        public ObservableCollection<DepartmentModel> Departements
        {
            get { return m_Departements; }
            set {
                if (value != m_Departements)
                {
                    m_Departements = value;
                    OnPropertyChanged();
                }
            }
        }

        private DepartmentModel m_SelectedDepartement;
        public DepartmentModel SelectedDepartement
        {
            get { return m_SelectedDepartement; }
            set
            {
                if (value != m_SelectedDepartement)
                {
                    m_SelectedDepartement = value;
                    Employee.Manager = value.ManagerName;
                    OnPropertyChanged();
                }
            }
        }

        private EmployeeEditView m_Control;

        #endregion

        #region Constructor

        public EmployeeEditViewModel(int key, EmployeeEditView control) : base(key)
        {
            m_Control = control;
            Departements = new ObservableCollection<DepartmentModel>();
            LoadDepartements();
            CreateCommands();
        }

        #endregion

        #region Commands

        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand DeleteCommand { get; private set; }

        private void CreateCommands()
        {
            SaveCommand = new RelayCommand(Save);
            DeleteCommand = new RelayCommand(Delete);
        }

        private void Save(object o = null)
        {
            var param = new ObservableCollection<SqlParameter>();
            param.Add(new SqlParameter("@nKey", Employee.Key));
            param.Add(new SqlParameter("@nEmployeeNmb", Employee.EmplyeeNmb));
            param.Add(new SqlParameter("@szTelephone", Employee.LandlineNbr));
            param.Add(new SqlParameter("@szMail", Employee.EMail));
            param.Add(new SqlParameter("@nDepartementLink", SelectedDepartement.Key));
            param.Add(new SqlParameter("@szJobName", Employee.Job));
            param.Add(new SqlParameter("@dtRecruitDate", Employee.HireDate));
            param.Add(new SqlParameter("@szTelephonePrivate", Employee.LandlineNmbPrivate));
            param.Add(new SqlParameter("@dtBirthdate", Employee.Birthday));
            param.Add(new SqlParameter("@szSex", Employee.Sex));

            DBProvider.ExecProcedure("sp_AlterEmployee", param);
        }

        private async void Delete(object o = null)
        {
            DeleteEmployeeDialog dialog = new DeleteEmployeeDialog();
            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                var param = new ObservableCollection<SqlParameter>();
                param.Add(new SqlParameter("@nKey", Employee.Key));
                DBProvider.ExecProcedure("sp_DeleteEmployee", param);
            }
        }

        #endregion

        #region LoadData

        private void LoadDepartements()
        {
            Departements.Clear();
            var data = DBProvider.ExecProcedure("sp_LoadDepartements");
            foreach (DataRow row in data.Rows)
            {
                var newDep = new DepartmentModel()
                {
                    Key = (int)row["nKey"],
                    Name = row["szName"].ToString(),
                    Identifier = row["szIdentifier"].ToString(),
                    Info = row["szInfo"].ToString(),
                    ManagerLink = (int)row["nManagerLink"],
                    ManagerName = row["szManagerName"].ToString()
                };
                Departements.Add(newDep);
                if (newDep.Name == Employee.Department)
                    SelectedDepartement = newDep;
            }
        }

        #endregion
    }
}
