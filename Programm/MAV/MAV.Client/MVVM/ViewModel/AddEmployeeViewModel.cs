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

        private ObservableCollection<DepartmentModel> departements;
        //Liste aller Abteilungen aus DB
        public ObservableCollection<DepartmentModel> Departements
        {
            get { return departements; }
            set
            {
                if (value != departements)
                {
                    departements = value;
                    OnPropertyChanged();
                }
            }
        }               

        private DepartmentModel selectedDepartement;
        public DepartmentModel SelectedDepartement
        {
            get { return selectedDepartement; }
            set
            {
                if (value != selectedDepartement)
                {
                    selectedDepartement = value;
                    OnPropertyChanged();
                }
            }
        }

        private AddEmployeeView control;
        public AddEmployeeView Control
        {
            get { return control; }
            private set
            {
                if (value != control)
                {
                    control = value;
                    OnPropertyChanged();
                }
            }
        }

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (value != firstName)
                {
                    firstName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                if (value != lastName)
                {
                    lastName = value;
                    OnPropertyChanged();
                }
            }
        }

        private int generatedPersNr;

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

        /// <summary>
        /// Hinzufügen eines neuen Mitarbeiters zur Datenbank, falls alle Eingaben gemacht wurden
        /// </summary>
        /// <param name="parameter"></param>
        private void AddEmployee(object parameter = null)
        {
            bool persNrNotAvailable = false;

            // Fehlermeldung (Dialog-Fenster), falls notwendige Daten zum Hinzufügen eines Mitarbeiters fehlen 
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
                else // Ansonsten die Eingabe (Wunschpersonalnummer) prüfen und verwenden
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

                //Alternatives Dialog-Fenster, wenn die gewünschte Pers.Nr. nicht verfügbar war und die Generierte verwendet wurde
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
        /// Prüfen, ob die eingegebene PersNr bereits vergeben ist
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

        /// <summary>
        /// Beim Aufrufen erscheint ein Dialog-Fenster
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

        /// <summary>
        /// Alle Felder zur Eingabe von Daten werden wieder auf Startzustand zurückgesetzt
        /// </summary>
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
