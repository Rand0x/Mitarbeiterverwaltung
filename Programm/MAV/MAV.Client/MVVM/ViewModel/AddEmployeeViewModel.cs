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
    class AddEmployeeViewModel : ViewModelBase
    {
        #region Properties

        private ObservableCollection<DepartmentModel> m_Departements;
        //Liste aller Abteilungen aus DB
        public ObservableCollection<DepartmentModel> Departements
        {
            get { return m_Departements; }
            set
            {
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

        private AddEmployeeView m_Control;
        public AddEmployeeView Control
        {
            get { return m_Control; }
            private set
            {
                if (value != m_Control)
                {
                    m_Control = value;
                    OnPropertyChanged();
                }
            }
        }

        private string m_FirstName;
        public string FirstName
        {
            get { return m_FirstName; }
            set
            {
                if (value != m_FirstName)
                {
                    m_FirstName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string m_LastName;
        public string LastName
        {
            get { return m_LastName; }
            set
            {
                if (value != m_LastName)
                {
                    m_LastName = value;
                    OnPropertyChanged();
                }
            }
        }

        int generatedPersNr;

        #endregion

        #region Constructor

        public AddEmployeeViewModel(AddEmployeeView control, UserModel user) : base(user)
        {
            Control = control;

            Departements = new ObservableCollection<DepartmentModel>();
            FirstName = string.Empty;
            LastName = string.Empty;

            GeneratePersNr();  // Gleich zu Beginn wird eine mögliche PersNr generiert 
            LoadDepartements();
            CreateCommands();
        }

        #endregion

        #region Commands

        public RelayCommand AddEmployeeCommand { get; private set; }

        private void CreateCommands()
        {
            AddEmployeeCommand = new RelayCommand(AddEmployee);
        }

        #endregion

        #region Implementation

        private void AddEmployee(object parameter = null)
        {
            bool persNrNotAvailable = false;

            if (LastName is null || LastName == String.Empty ||  FirstName is null || FirstName == String.Empty || Control.cbxSex.SelectedItem is null
                || Control.dpBirthday.SelectedDate is null || Control.dpHireDate.SelectedDate is null || SelectedDepartement is null)
            {
                DialogPopUp("Daten nicht vollständig", "Es müssen alle Felder ausgefüllt werden");
            }
            else
            {
                int persNr;

                if (Control.CheckboxPersNr.IsChecked == true)  // Prüfen ob die Generierte Personalnummer verwendet werden soll
                {
                    persNr = generatedPersNr;
                }
                else // Ansonsten die Eingabe Prüfen und verwenden
                {
                    try
                    {
                        persNr = Int32.Parse(Control.TextBoxPersNr.Text);
                        // Wenn die gewünschte Personalnummer bereits vorhanden ist, wird einfach die generierte verwendet 
                        if (!CheckPersNrAvailability(persNr)) 
                        {
                            persNrNotAvailable = true;
                            persNr = generatedPersNr;
                        }
                    }
                    catch (FormatException)
                    {
                        DialogPopUp("Fehlerhafte Daten", "Die Daten entsprechen nicht dem gewünschten Format");
                        return;
                    }                    
                }

                string eMail = ($"{FirstName.ToLower()}.{LastName.ToLower()}@mav.de");

                var param = new ObservableCollection<SqlParameter>();

                param.Add(new SqlParameter("@szFirstName", FirstName));
                param.Add(new SqlParameter("@szLastName", LastName));
                param.Add(new SqlParameter("@szMail", eMail));                
                param.Add(new SqlParameter("@nEmployeeNumber", persNr));
                param.Add(new SqlParameter("@nDepartementLink", SelectedDepartement.Key));
                param.Add(new SqlParameter("@dtBirthdate", Control.dpBirthday.SelectedDate));
                param.Add(new SqlParameter("@dtRecruitDate", Control.dpHireDate.SelectedDate));
                param.Add(new SqlParameter("@szSex", Control.cbxSex.SelectedItem.ToString().Substring(0,1))); //in DB wird nur der jeweils erste Buchstabe gespeichert (m/w/d)

                try
                {
                    DBProvider.ExecProcedure("sp_CreateEmployee", param);
                }
                catch (Exception ex)
                {
                    DialogPopUp("Fehler", ex.Message);
                    ClearBoxes();
                    return;
                }

                if(persNrNotAvailable)
                    DialogPopUp("Erfolgreich hinzugefügt", "Gewünschte Mitarbeiternummer ist leider nicht verfügbar.",
                    $"{Control.FirstName.Text} {Control.LastName.Text} wurde mit generierter Mitarbeiternummer {persNr} angelegt");
                else
                    DialogPopUp("Erfolgreich hinzugefügt", "Es wurde ein neuer Mitarbeiter angelegt:",
                    $"{Control.FirstName.Text} {Control.LastName.Text} mit Mitarbeiternummer {persNr}");

                ClearBoxes();
            }
        }


        /// <summary>
        /// Funktion zum Generieren einer neuen Personalnummer
        /// </summary>
        private void GeneratePersNr()
        {
            DataTable data;

            try
            {
                data = DBProvider.ExecProcedure("sp_LoadEmployeeNumbers");
            }
            catch (Exception ex)
            {
                DialogPopUp("Fehler", ex.Message);
                return;
            }

            int highestPersNr = 0;

            foreach (DataRow row in data.Rows)
            {
                int current = (int)row["nEmployeeNumber"];

                if (current > highestPersNr)
                    highestPersNr = current;
            }
            generatedPersNr = highestPersNr + 1;
        }

        /// <summary>
        /// Prüfen, ob die ingegebene PersNr bereits vergeben wurde
        /// </summary>
        /// <param name="choosenPersNr">Eingegebene Personalnummer</param>
        /// <returns></returns>
        private bool CheckPersNrAvailability(int choosenPersNr)
        {
            bool available = true;

            if (choosenPersNr <= 0)
                available = false;
            else
            {
                DataTable data;
                try
                {
                    data = DBProvider.ExecProcedure("sp_LoadEmployeeNumbers");
                }
                catch (Exception ex)
                {
                    DialogPopUp("Fehler", ex.Message);
                    return false;
                }

                foreach (DataRow row in data.Rows)
                {
                    int current = (int)row["nEmployeeNumber"];

                    if (current == choosenPersNr)
                        available = false;
                }
            }            
            return available;
        }


        private void DialogPopUp(string title, string firstLine, string secondLine = "")
        {
            Dialog dialog = new Dialog();
            dialog.Title = title;
            dialog.FirstLineContent = firstLine;
            dialog.SecondLineText = secondLine;
            var result = dialog.ShowAsync();
        }

        private void ClearBoxes()
        {
            Control.FirstName.Text = null;
            Control.LastName.Text = null;
            Control.TextBoxPersNr.Text = null;
            Control.cbxSex.SelectedItem = null;
            Control.dpBirthday.SelectedDate = null;
            Control.dpHireDate.SelectedDate = DateTime.Today;
            Control.cmbDepartments.SelectedItem = null;
            Control.CheckboxPersNr.IsChecked = true;
        }

        #endregion

        #region LoadData

        /// <summary>
        /// Laden aller Abteilungen aus der DB
        /// </summary>
        private void LoadDepartements()
        {
            DataTable data;

            try
            {
                data = DBProvider.ExecProcedure("sp_LoadDepartements");
            }
            catch (Exception ex)
            {
                DialogPopUp("Fehler", ex.Message);
                return;
            }

            Departements.Clear();

            //hinzufügen zur Liste
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
            }
        }
        #endregion
    }

}
