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

            FirstName = string.Empty;
            LastName = string.Empty;

            GeneratePersNr();  // Gleich zu Beginn wird eine mögliche PersNr generiert 
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

        private void AddEmployee(object parameter = null)
        {
            if (LastName is null || LastName == String.Empty ||  FirstName is null || FirstName == String.Empty || Control.cbxSex.SelectedItem is null 
                || Control.dpBirthday.SelectedDate is null || Control.dpHireDate.SelectedDate is null)
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
                            persNr = generatedPersNr;
                        }
                    }
                    catch (FormatException e)
                    {
                        DialogPopUp("Fehlerhafte Daten", "Die Daten entsprechen nicht dem gewünschten Format");
                        return;
                    }                    
                }

                var param = new ObservableCollection<SqlParameter>();

                param.Add(new SqlParameter("@szFirstName", FirstName));
                param.Add(new SqlParameter("@szLastName", LastName));
                param.Add(new SqlParameter("@nEmployeeNumber", persNr));
                param.Add(new SqlParameter("@dtBirthdate", Control.dpBirthday.SelectedDate));
                param.Add(new SqlParameter("@dtRecruitDate", Control.dpHireDate.SelectedDate));
                param.Add(new SqlParameter("@szSex", Control.cbxSex.SelectedItem));

                try
                {
                    DBProvider.ExecProcedure("sp_CreateEmployee", param);
                }
                catch (Exception ex)
                {
                    DialogPopUp("Fehler", ex.Message);
                    //ClearBoxes();
                    return;
                }

                DialogPopUp("Erfolgreich hinzugefügt", "Es wurde ein neuer Mitarbeiter angelegt", 
                    $"{Control.FirstName.Text} {Control.LastName.Text} mit Mitarbeiternummer {persNr}");
                //ClearBoxes();
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
            generatedPersNr = highestPersNr++;
        }

        /// <summary>
        /// Prüfen, ob die ingegebene PersNr bereits vergeben wurde
        /// </summary>
        /// <param name="choosenPersNr">Eingegebene Personalnummer</param>
        /// <returns></returns>
        private bool CheckPersNrAvailability(int choosenPersNr)
        {
            bool available = true;
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
    }
    
}
