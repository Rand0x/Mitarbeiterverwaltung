using MAV.Base;
using MAV.Client.MVVM.Model;
using MAV.Client.MVVM.View;
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
        //Liste aller Abteilungen aus DB
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

        public EmployeeEditViewModel(int key, EmployeeEditView control, ClientViewModel clientVM) : base(key, clientVM)
        {
            m_Control = control;
            Departements = new ObservableCollection<DepartmentModel>();
            LoadDepartements();
            CreateCommands();
        }

        #endregion

        #region Commands

        //Commands zum hinzufügen und Löschen von Mitarbeitern
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand DeleteCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }

        private void CreateCommands()
        {
            SaveCommand = new RelayCommand(Save);
            DeleteCommand = new RelayCommand(Delete);
            CancelCommand = new RelayCommand((object o) => 
            {
                ClientVM.DirectoryViewCommand.Execute(null);
            });
        }

        /// <summary>
        /// Ändern der Daten eines Mitarbeiters
        /// </summary>
        /// <param name="o"></param>
        private void Save(object o = null)
        {
            //zusammenstellen der benötigten Parameter für Prozeduraufruf
            var param = new ObservableCollection<SqlParameter>();
            param.Add(new SqlParameter("@nKey", Employee.Key));
            param.Add(new SqlParameter("@nEmployeeNmb", Employee.EmplyeeNmb));
            param.Add(new SqlParameter("@szTelephone", Employee.LandlineNbr));
            param.Add(new SqlParameter("@szMail", Employee.EMail));
            param.Add(new SqlParameter("@nDepartementLink", SelectedDepartement.Key));
            param.Add(new SqlParameter("@szJobName", Employee.Job));
            param.Add(new SqlParameter("@dtRecruitDate", Employee.HireDate));
            //param.Add(new SqlParameter("@szTelephonePrivate", Employee.LandlineNmbPrivate));
            param.Add(new SqlParameter("@dtBirthdate", Employee.Birthday));
            param.Add(new SqlParameter("@szSex", Employee.Sex));


            try
            {
                DBProvider.ExecProcedure("sp_AlterEmployee", param);
                //ToDo einfügen
                //SaveWarnings();
                //SaveBonusPayment();
            }
            catch (Exception ex)
            {
                DialogPopUp("Fehler", ex.Message);
            }

            DialogPopUp("Erfolgreich geändert", $"Daten von {Employee.FirstName} {Employee.LastName} wurden geändert.");

            //zur Adressliste zurückkehren
            ClientVM.DirectoryViewCommand.Execute(null);
        }

        /// <summary>
        /// speichert die geänderten Warnungen falls vorhanden
        /// </summary>
        private void SaveWarnings()
        {
            foreach (var warning in Employee.WarningsList)
            {
                var param = new ObservableCollection<SqlParameter>();
                param.Add(new SqlParameter("@nKey", warning.Key));
                param.Add(new SqlParameter("@szReason", warning.Reason));
                param.Add(new SqlParameter("@dtIssueDate", warning.IssueDate));
                param.Add(new SqlParameter("@szComment", warning.Comment));

                try
                {
                    DBProvider.ExecProcedure("sp_AlterWarning", param);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// speichert die geänderten Bonuszahlungen falls vorhanden
        /// </summary>
        private void SaveBonusPayment()
        {
            foreach (var payment in Employee.BonusPaymentList)
            {
                var param = new ObservableCollection<SqlParameter>();
                param.Add(new SqlParameter("@nKey", payment.Key));
                param.Add(new SqlParameter("@szReason", payment.Reason));
                param.Add(new SqlParameter("@rAmount", payment.Amount));
                param.Add(new SqlParameter("@dtIssueDate", payment.DateOfPayment));
                param.Add(new SqlParameter("@szComment", payment.Comment));

                try
                {
                    DBProvider.ExecProcedure("sp_AlterWarning", param);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Löschen eines Mitarbeiters aus der DB
        /// </summary>
        /// <param name="o"></param>
        private async void Delete(object o = null)
        {
            //Abfrage ob wirklich gelöscht werden soll
            DeleteEmployeeDialog dialog = new DeleteEmployeeDialog();
            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                //aus DB löschen
                var param = new ObservableCollection<SqlParameter>();
                param.Add(new SqlParameter("@nKey", Employee.Key));

                try
                {
                    DBProvider.ExecProcedure("sp_DeleteEmployee", param);
                }
                catch(Exception ex)
                {
                    DialogPopUp("Fehler", ex.Message);
                }
            }

            DialogPopUp("Erfolgreich gelöscht", $"Daten von {Employee.FirstName} {Employee.LastName} wurden gelöscht.");

            //zur Adressliste zurückkehren
            ClientVM.DirectoryViewCommand.Execute(null);
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
            catch(Exception ex)
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
                if (newDep.Name == Employee.Department)
                    SelectedDepartement = newDep;
            }
        }

        #endregion
    }
}
