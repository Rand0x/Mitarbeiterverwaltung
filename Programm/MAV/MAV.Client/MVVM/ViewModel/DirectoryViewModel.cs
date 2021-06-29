using MAV.Base;
using MAV.Client.MVVM.Model;
using MAV.Client.MVVM.View;
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
        //Liste aller Elemente in der Adressliste
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
        //Liste aller Abteilungen
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

        private ClientViewModel m_ClientVM;
        //ViewModel des Clients
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
        }

        #endregion

        #region LoadData

        /// <summary>
        /// Lädt alle Abteilungen aus DB
        /// </summary>
        private void LoadDepartements()
        {
            DataTable data;

            try
            {
                data = DBProvider.ExecProcedure("sp_LoadDepartements");
            }
            catch(Exception ex)
            {
                DialogPopUp("Fehler", ex.Message);
                return;
            }

            Departements.Clear();

            //Defualt Abteilungen die alle vorhanden auswählt
            var defaultItem = new DepartmentModel()
            {
                Key = -1,
                Name = "Alle"
            };
            Departements.Add(defaultItem);
            SelectedDepartement = defaultItem;

            //hinzufügen der geladenen zu Liste
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

        /// <summary>
        /// Laden aller Einträge der Adressliste
        /// </summary>
        /// <param name="p"></param>
        public void LoadAddressList(object p = null)
        {
            var param = new ObservableCollection<SqlParameter>();
            DataTable data;

            //Filter falls vorhanden hinzufügen
            if (p != null)
                param.Add(new SqlParameter("@szFirstName", p.ToString()));
            if (SelectedDepartement != null && SelectedDepartement.Key != -1)
                param.Add(new SqlParameter("@nDepartementLink", SelectedDepartement.Key));

            try
            {
                data = DBProvider.ExecProcedure("sp_LoadAddressList", param);
            }
            catch(Exception ex)
            {
                DialogPopUp("Fehler", ex.Message);
                return;
            }

            AddressList.Clear();

            //Hinzufügen der Daten zu Liste
            foreach (DataRow row in data.Rows)
            {
                AddressList.Add(new AddressEntryModel()
                {
                    Key = (int)row["nKey"],
                    FirstName = row["szFirstName"].ToString(),
                    LastName = row["szLastName"].ToString(),
                    LandlineNbr = row["szLandlineNmb"].ToString(),
                    EmployeeNumber = (int)row["nEmployeeNumber"],
                    Department = row["szDepartement"].ToString()
                });
            }

            //Sortieren nach Vornamen
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

        public void OrderByEmployeeNbr()
        {
            AddressList = AddressList.OrderBy(o => o.EmployeeNumber).ToList();
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

        #region Dialog

        /// <summary>
        /// Beim Aufrufen erscheint ein Fenster
        /// </summary>
        /// <param name="title">Title des Fensters</param>
        /// <param name="firstLine">Erste Zeile des Fensters</param>
        /// <param name="secondLine">Zweite Zeile des Fensters</param>
        private void DialogPopUp(string title, string firstLine, string secondLine = "")
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
