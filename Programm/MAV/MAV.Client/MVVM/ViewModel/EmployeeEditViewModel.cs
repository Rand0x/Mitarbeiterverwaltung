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
using System.Windows.Documents;

namespace MAV.Client.MVVM.ViewModel
{
    public class EmployeeEditViewModel : EmployeeInfoViewModel
    {
        #region Properties
        private ObservableCollection<WarningModel> addedWarningsList = new ObservableCollection<WarningModel>();
        public ObservableCollection<WarningModel> AddedWarningsList
        {
            get { return addedWarningsList; }
            set
            {
                addedWarningsList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<BonusPaymentModel> addedBonusPaymentList = new ObservableCollection<BonusPaymentModel>();
        public ObservableCollection<BonusPaymentModel> AddedBonusPaymentList
        {
            get { return addedBonusPaymentList; }
            set
            {
                addedBonusPaymentList = value;
                OnPropertyChanged();
            }
        }

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

            if (Employee.BonusPaymentList.Count >= 3)
                m_Control.AddBonusPaymentButton.Visibility = System.Windows.Visibility.Collapsed;
            if (Employee.WarningsList.Count >= 3)
                m_Control.WarningButton.Visibility = System.Windows.Visibility.Collapsed;            
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

        public void updateRichTextBoxContent()
        {
            m_Control.commentBox.Document.Blocks.Clear();
            m_Control.commentBox.Document.Blocks.Add(new Paragraph(new Run(Employee.Comment)));
        }

        /// <summary>
        /// Ändern der Daten eines Mitarbeiters
        /// </summary>
        /// <param name="o"></param>
        private void Save(object o = null)
        {
            // Kein Binding möglich
            Employee.Comment = new TextRange(m_Control.commentBox.Document.ContentStart, m_Control.commentBox.Document.ContentEnd).Text;

            //zusammenstellen der benötigten Parameter für Prozeduraufruf
            var param = new ObservableCollection<SqlParameter>();
            param.Add(new SqlParameter("@nKey", Employee.Key));          
            param.Add(new SqlParameter("@nEmployeeNmb", Employee.EmplyeeNmb));
            if(Employee.LandlineNbr != null)
                param.Add(new SqlParameter("@szTelephone", Employee.LandlineNbr));
            if(Employee.EMail != null)
                param.Add(new SqlParameter("@szMail", Employee.EMail));
            if(SelectedDepartement != null)
                param.Add(new SqlParameter("@nDepartementLink", SelectedDepartement.Key));
            if(Employee.Job != null)
                param.Add(new SqlParameter("@szJobName", Employee.Job));
            if(Employee.HireDate != null)
                param.Add(new SqlParameter("@dtRecruitDate", Employee.HireDate));
            if(Employee.LandlineNmbPrivate != null)
                param.Add(new SqlParameter("@szTelephonePrivate", Employee.LandlineNmbPrivate));
            if(Employee.Birthday != null)
                param.Add(new SqlParameter("@dtBirthdate", Employee.Birthday));
            param.Add(new SqlParameter("@szSex", Employee.SexEn.ToString().Substring(0,1).ToUpper()));
            if(Employee.NoticePeriod != null)
                param.Add(new SqlParameter("@nNoticePeriod", Employee.NoticePeriod));
            if(Employee.HoursPerWeek != null)
                param.Add(new SqlParameter("@nHoursPerWeek", Employee.HoursPerWeek));
            if(Employee.Overtime != null)
                param.Add(new SqlParameter("@rOvertime", Employee.Overtime));
            if(Employee.Wage != null)
                param.Add(new SqlParameter("@rWage", Employee.Wage));
            if(Employee.HolidayPerYear != null)
                param.Add(new SqlParameter("@nHolidyPerYear", Employee.HolidayPerYear));
            if(Employee.TaxClass != null)
                param.Add(new SqlParameter("@nTaxClass", Employee.TaxClass));
            if(Employee.HouseNumber != null)
                param.Add(new SqlParameter("@szHouseNumber", Employee.HouseNumber));
            if(Employee.Street != null)
                param.Add(new SqlParameter("@szStreet", Employee.Street));
            if(Employee.PLZ != null)
                param.Add(new SqlParameter("@zPLZ", Employee.PLZ));
            if(Employee.City != null)
                param.Add(new SqlParameter("@szCity", Employee.City));
            if(Employee.IBAN != null)
                param.Add(new SqlParameter("@szIBAN", Employee.IBAN));
            if(Employee.BIC != null)
                param.Add(new SqlParameter("@szBIC", Employee.BIC));
            if(Employee.BankName != null)
                param.Add(new SqlParameter("@szBankName", Employee.BankName));

            try
            {
                DBProvider.ExecProcedure("sp_AlterEmployee", param);

                SaveChangedWarnings();
                SaveChangedBonusPayments();

                AddWarnings();
                AddBonusPayments();
            }
            catch (Exception ex)
            {
                DialogPopUp("Fehler", ex.Message);
                return;
            }            

            DialogPopUp("Erfolgreich geändert", $"Daten von {Employee.FirstName} {Employee.LastName} wurden geändert.");

            //zur Adressliste zurückkehren
            ClientVM.DirectoryViewCommand.Execute(null);
        }

        private void AddWarnings()
        {
            foreach (var warning in AddedWarningsList)
            {
                var param = new ObservableCollection<SqlParameter>();
                param.Add(new SqlParameter("@nEmployeeLink", Employee.Key));
                param.Add(new SqlParameter("@szReason", warning.Reason));
                param.Add(new SqlParameter("@dtIssueDate", warning.IssueDate));
                param.Add(new SqlParameter("@szComment", warning.Comment));

                try
                {
                    DBProvider.ExecProcedure("sp_AddWarning", param);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// speichert die geänderten Warnungen falls vorhanden
        /// </summary>
        private void SaveChangedWarnings()
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

        private void AddBonusPayments()
        {
            foreach (var payment in AddedBonusPaymentList)
            {
                var param = new ObservableCollection<SqlParameter>();
                param.Add(new SqlParameter("@nEmployeeLink", Employee.Key));
                param.Add(new SqlParameter("@szReason", payment.Reason));
                param.Add(new SqlParameter("@rAmount", payment.Amount));
                param.Add(new SqlParameter("@dtDateOfPayment", payment.DateOfPayment));
                param.Add(new SqlParameter("@szComment", payment.Comment));

                try
                {
                    DBProvider.ExecProcedure("sp_AddBonusPayment", param);
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
        private void SaveChangedBonusPayments()
        {
            foreach (var payment in Employee.BonusPaymentList)
            {
                var param = new ObservableCollection<SqlParameter>();
                param.Add(new SqlParameter("@nKey", payment.Key));
                param.Add(new SqlParameter("@szReason", payment.Reason));
                param.Add(new SqlParameter("@rAmount", payment.Amount));
                param.Add(new SqlParameter("@dtDateOfPayment", payment.DateOfPayment));
                param.Add(new SqlParameter("@szComment", payment.Comment));

                try
                {
                    DBProvider.ExecProcedure("sp_AlterBonusPayment", param);
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
                    return;
                }

                DialogPopUp("Erfolgreich gelöscht", $"Daten von {Employee.FirstName} {Employee.LastName} wurden gelöscht.");
            }
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

        public void AddFieldForWarning()
        {
            AddedWarningsList.Add(new WarningModel { IssueDate = DateTime.Today });
            if (Employee.WarningsList.Count + AddedWarningsList.Count >= 3)
                m_Control.WarningButton.Visibility = System.Windows.Visibility.Collapsed;
        }


        public void AddFieldForBonusPayment()
        {
            AddedBonusPaymentList.Add(new BonusPaymentModel { DateOfPayment = DateTime.Today });
            if (Employee.BonusPaymentList.Count + AddedBonusPaymentList.Count >= 3)
                m_Control.AddBonusPaymentButton.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
