﻿using MAV.Base;
using MAV.DirectoryModule.Model;
using MAV.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MAV.Client.MVVM.ViewModel
{
    public class DirectoryViewModel : ViewModelBase
    {
        #region Properties

        private List<AddressEntryModel> m_AddressList;
        public List<AddressEntryModel> AddressList
        {
            get { return m_AddressList; }
            set
            {
                if (value != m_AddressList)
                {
                    m_AddressList = value;
                    OnPropertyChanged();
                }
            }
        }

        private AddressEntryModel m_SelectedEmployee;
        public AddressEntryModel SelectedEmployee
        {
            get { return m_SelectedEmployee; }
            set
            {
                if (value != m_SelectedEmployee)
                {
                    m_SelectedEmployee = value;
                    OnPropertyChanged();
                }
            }
        }

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
                    OnPropertyChanged();
                }
            }
        }

        private string m_SearchText;
        public string SearchText
        {
            get { return m_SearchText; }
            set
            {
                if (value != m_SearchText)
                {
                    m_SearchText = value;
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

        public DirectoryViewModel(UserModel user, ClientViewModel clientVM) : base(user)
        {
            ClientVM = clientVM;
            CreateCommands();

            AddressList = new List<AddressEntryModel>();
            LoadAddressList();
            SelectedEmployee = null;

            Departements = new ObservableCollection<DepartmentModel>();
            LoadDepartements();

            SearchText = null;
        }

        #endregion

        #region LoadData

        private void LoadDepartements()
        {
            Departements.Clear();

            var defaultItem = new DepartmentModel()
            {
                Key = -1,
                Name = "Alle"
            };
            Departements.Add(defaultItem);
            SelectedDepartement = defaultItem;

            var data = DBProvider.ExecProcedure("sp_LoadDepartements");
            foreach(DataRow row in data.Rows)
            {
                Departements.Add(new DepartmentModel()
                {
                    Key = (int)row["nKey"],
                    Name = row["szName"].ToString(),
                    Identifier = row["szIdentifier"].ToString(),
                    Info = row["szInfo"].ToString(),
                    ManagerLink = (int)row["nManagerLink"],
                    ManagerName = row["szManagerName"].ToString()
                });
            }
        }

        public void LoadAddressList(object p = null)
        {
            AddressList.Clear();

            var param = new ObservableCollection<SqlParameter>();

            if (SearchText != null)
                param.Add(new SqlParameter("@szFirstName", SearchText));
            if (SelectedDepartement != null && SelectedDepartement.Key != -1)
                param.Add(new SqlParameter("@nDepartementLink", SelectedDepartement.Key));

            var data = DBProvider.ExecProcedure("sp_LoadAddressList", param);

            foreach (DataRow row in data.Rows)
            {
                AddressList.Add(new AddressEntryModel()
                {
                    Key = (int)row["nKey"],
                    FirstName = row["szFirstName"].ToString(),
                    LastName = row["szLastName"].ToString(),
                    LandlineNbr = row["szTelephone"].ToString(),
                    MobileNbr = row["szTelephone"].ToString(),
                    Department = row["szDepartement"].ToString()
                });
            }

            OrderByFirstName();
        }

        #endregion

        #region Order

        public void OrderByFirstName()
        {
            AddressList = AddressList.OrderBy(o => o.FirstName).ToList();
        }

        public void OrderByLastName()
        {
            AddressList = AddressList.OrderBy(o => o.LastName).ToList();
        }

        public void OrderByDepartment()
        {
            AddressList = AddressList.OrderBy(o => o.Department).ToList();
        }

        public void OrderByLandlineNbr()
        {
            AddressList = AddressList.OrderBy(o => o.LandlineNbr).ToList();
        }

        public void OrderByMobileNbr()
        {
            AddressList = AddressList.OrderBy(o => o.MobileNbr).ToList();
        }

        #endregion

        #region Commands

        public RelayCommand EmployeeInfoViewCommand { get; private set; }
        public RelayCommand EmployeeEditViewCommand { get; private set; }
        public RelayCommand LoadAddressListCommand { get; private set; }

        private void CreateCommands()
        {
            LoadAddressListCommand = new RelayCommand(LoadAddressList);
            EmployeeEditViewCommand = new RelayCommand((o) => { 
                if(SelectedEmployee != null)
                    ClientVM.EmployeeEditViewCommand.Execute(SelectedEmployee.Key); 
            });
            EmployeeInfoViewCommand = new RelayCommand((o) => { 
                if(SelectedEmployee != null)
                    ClientVM.EmployeeInfoViewCommand.Execute(SelectedEmployee.Key); 
            });
        }

        #endregion
    }
}
