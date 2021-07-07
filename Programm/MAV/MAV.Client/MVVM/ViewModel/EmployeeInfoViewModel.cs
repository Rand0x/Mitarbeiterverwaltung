using MAV.Base;
using MAV.Client.MVVM.Model;
using MAV.Client.MVVM.View;
using MAV.Helper;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace MAV.Client.MVVM.ViewModel
{
    public class EmployeeInfoViewModel : PropertyChangedBase
    {
        #region Properties

        private EmployeeModel employee;
        //ausgewählter Mitarbeiter
        public EmployeeModel Employee
        {
            get { return employee; }
            set
            {
                if (value != employee)
                {
                    employee = value;
                    OnPropertyChanged();
                }
            }
        }

        private ClientViewModel clientVM;
        public ClientViewModel ClientVM
        {
            get { return clientVM; }
            set
            {
                if (value != clientVM)
                {
                    clientVM = value;
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

        #region LoadData

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
            catch (Exception ex)
            {
                DialogPopUp("Fehler", ex.Message);
                return;
            }

            //Mitarbeiter hinzufügen
            foreach (DataRow row in data.Rows)
            {
                Employee = new EmployeeModel()
                {
                    Key = (int)row["nKey"],
                    FirstName = row["szFirstName"].ToString(),
                    LastName = row["szLastName"].ToString(),
                    EmplyeeNmb = (int)row["nEmployeeNumber"],
                    LandlineNbr = row["szTelephone"].ToString(),
                    MobileNbr = row["szMobileNumber"].ToString(),
                    EMail = row["szMail"].ToString(),
                    Department = row["szDepartementName"].ToString(),
                    Job = row["szJobName"].ToString(),
                    HireDate = DateTime.Parse(row["dtRecruitDate"].ToString()),
                    Manager = row["szManager"].ToString(),
                    LandlineNmbPrivate = row["szPrivateTelephone"].ToString(),
                    MobileNbrPrivate = row["szPrivateMobileNmb"].ToString(),
                    Birthday = DateTime.Parse(row["dtBirthdate"].ToString()),
                    Street = row["szStreet"].ToString(),
                    HouseNumber = row["szHouseNumber"].ToString(),
                    PLZ = row["szPLZ"].ToString(),
                    City = row["szCity"].ToString(),
                    IBAN = row["szIBAN"].ToString(),
                    BIC = row["szBIC"].ToString(),
                    Comment = row["szComment"].ToString(),
                    BankName = row["szBankName"].ToString(),
                    NoticePeriod = row["nNoticePeriod"] is DBNull ? null : (int?)row["nNoticePeriod"],
                    HoursPerWeek = row["nHoursPerWeek"] is DBNull ? null : (int?)row["nHoursPerWeek"],
                    Overtime = row["rOvertime"] is DBNull ? null : (double?)double.Parse(row["rOvertime"].ToString()),
                    Wage = row["rWage"] is DBNull ? null : (double?)double.Parse(row["rWage"].ToString()),
                    HolidayPerYear = row["nHolidyPerYear"] is DBNull ? null : (int?)row["nHolidyPerYear"],
                    TaxClass = row["nTaxClass"] is DBNull ? null : (int?)row["nTaxClass"],
                    MaritalStatus = row["szMaritalStatus"].ToString()
                };

                // In Datenbank wird für Geschlecht nur ein Char gespeichert (m/w/d)
                // Daher hier die Zusweisung mit Wert aus Enum
                string sex = row["szSex"].ToString();
                if (sex != null)
                {
                    switch (sex[0])
                    {
                        case 'm':
                            Employee.Sex = Gender.männlich;
                            break;
                        case 'd':
                            Employee.Sex = Gender.divers;
                            break;
                        default:
                            Employee.Sex = Gender.weiblich;
                            break;
                    }
                }
            }
            LoadWarnings(key);
            LoadBonusPayments(key);
        }

        /// <summary>
        /// Laden der Abmahnungen des ausgewählten Mitarbeiters
        /// </summary>
        /// <param name="key"></param>
        private void LoadWarnings(int key)
        {
            DataTable data;
            var param = new ObservableCollection<SqlParameter>();
            param.Add(new SqlParameter("@nEmployeeLink", key));

            try
            {
                data = DBProvider.ExecProcedure("sp_LoadWarnings", param);
            }
            catch (Exception ex)
            {
                DialogPopUp("Fehler", ex.Message);
                return;
            }
            // Abmahnungen laden
            Employee.WarningsList = new ObservableCollection<WarningModel>();
            foreach (DataRow row in data.Rows)
            {
                Employee.WarningsList.Add(new WarningModel
                {
                    Key = (int)row["nKey"],
                    Reason = row["szReason"].ToString(),
                    IssueDate = DateTime.Parse(row["dtIssueDate"].ToString()),
                    Comment = row["szComment"].ToString()
                });
            }
        }

        /// <summary>
        /// Laden der Bonus-Zahlungen des ausgewählten Mitarbeiters
        /// </summary>
        /// <param name="key"></param>
        private void LoadBonusPayments(int key)
        {
            DataTable data;
            var param = new ObservableCollection<SqlParameter>();
            param.Add(new SqlParameter("@nEmployeeLink", key));

            try
            {
                data = DBProvider.ExecProcedure("sp_LoadBonusPayments", param);
            }
            catch (Exception ex)
            {
                DialogPopUp("Fehler", ex.Message);
                return;
            }
            // Abmahnungen laden
            Employee.BonusPaymentList = new ObservableCollection<BonusPaymentModel>();
            foreach (DataRow row in data.Rows)
            {
                Employee.BonusPaymentList.Add(new BonusPaymentModel
                {
                    Key = (int)row["nKey"],
                    Reason = row["szReason"].ToString(),
                    Amount = double.Parse(row["rAmount"].ToString()),
                    DateOfPayment = DateTime.Parse(row["dtDateOfPayment"].ToString()),
                    Comment = row["szComment"].ToString()
                });
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
