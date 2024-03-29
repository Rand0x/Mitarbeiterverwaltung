﻿using MAV.Base;
using System;
using System.Collections.ObjectModel;

public enum Gender { männlich, weiblich, divers }

namespace MAV.Client.MVVM.Model
{
    //Model Klasse um Benutzerdaten aus DB zu speichern
    public class EmployeeModel : PropertyChangedBase
    {
        private int key;
        public int Key
        {
            get { return key; }
            set
            {
                key = value;
                OnPropertyChanged();
            }
        }

        private int emplyeeNmb;
        public int EmplyeeNmb
        {
            get { return emplyeeNmb; }
            set
            {
                emplyeeNmb = value;
                OnPropertyChanged();
            }
        }

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged();
            }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged();
            }
        }

        private DateTime birthday;
        public DateTime Birthday
        {
            get { return birthday; }
            set
            {
                birthday = value;
                OnPropertyChanged();
            }
        }

        // Property zum Binden (OneWay) um Datum im richtigen (europäischen) Format darzustellen
        public string BirthDayAsString
        {
            get
            {
                return ($"{Birthday.Day.ToString("D2")}.{Birthday.Month.ToString("D2")}.{Birthday.Year}");
            }
        }

        private DateTime hiredate;
        public DateTime HireDate
        {
            get { return hiredate; }
            set
            {
                hiredate = value;
                OnPropertyChanged();
            }
        }

        // Property zum Binden (OneWay) um Datum im richtigen (europäischen) Format darzustellen
        public string HireDateAsString
        {
            get
            {
                return ($"{HireDate.Day.ToString("D2")}.{HireDate.Month.ToString("D2")}.{HireDate.Year}");
            }
        }

        private Gender sex;
        public Gender Sex
        {
            get
            {
                return sex;
            }
            set
            {
                sex = value;
                OnPropertyChanged();
            }
        }

        private string email;
        public string EMail
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        private string landlineNbr;
        public string LandlineNbr
        {
            get { return landlineNbr; }
            set
            {
                landlineNbr = value;
                OnPropertyChanged();
            }
        }

        private string mobileNbr;
        public string MobileNbr
        {
            get { return mobileNbr; }
            set
            {
                mobileNbr = value;
                OnPropertyChanged();
            }
        }

        private string job;
        public string Job
        {
            get { return job; }
            set
            {
                job = value;
                OnPropertyChanged();
            }
        }

        private int? hoursperweek;
        public int? HoursPerWeek
        {
            get { return hoursperweek; }
            set
            {
                hoursperweek = value;
                OnPropertyChanged();
            }
        }

        private double? overtime;
        public double? Overtime
        {
            get { return overtime; }
            set
            {
                overtime = value;
                OnPropertyChanged();
            }
        }

        private double? wage;
        public double? Wage
        {
            get { return wage; }
            set
            {
                wage = value;
                OnPropertyChanged();
            }
        }

        private int? holidayperyear;
        public int? HolidayPerYear
        {
            get { return holidayperyear; }
            set
            {
                holidayperyear = value;
                OnPropertyChanged();
            }
        }

        private int? noticeperiod;
        public int? NoticePeriod
        {
            get { return noticeperiod; }
            set
            {
                noticeperiod = value;
                OnPropertyChanged();
            }
        }

        private int? taxclass;
        public int? TaxClass
        {
            get { return taxclass; }
            set
            {
                taxclass = value;
                OnPropertyChanged();
            }
        }

        private string maritalStatus;
        public string MaritalStatus
        {
            get { return maritalStatus; }
            set
            {
                maritalStatus = value;
                OnPropertyChanged();
            }
        }

        private string comment;
        public string Comment
        {
            get { return comment; }
            set
            {
                if (value != null && value.Length > 2 && value.Substring(value.Length - 2).Contains("\r\n"))
                    comment = value.Substring(0, value.Length - 2);
                else
                    comment = value;
                OnPropertyChanged();
            }
        }

        private string street;
        public string Street
        {
            get { return street; }
            set
            {
                street = value;
                OnPropertyChanged();
            }
        }

        private string housenumber;
        public string HouseNumber
        {
            get { return housenumber; }
            set
            {
                housenumber = value;
                OnPropertyChanged();
            }
        }

        private string city;
        public string City
        {
            get { return city; }
            set
            {
                city = value;
                OnPropertyChanged();
            }
        }

        private string plz;
        public string PLZ
        {
            get { return plz; }
            set
            {
                plz = value;
                OnPropertyChanged();
            }
        }

        private string mailpirvate;
        public string MailPrivate
        {
            get { return mailpirvate; }
            set
            {
                mailpirvate = value;
                OnPropertyChanged();
            }
        }

        private string landlinenmbprivate;
        public string LandlineNmbPrivate
        {
            get { return landlinenmbprivate; }
            set
            {
                landlinenmbprivate = value;
                OnPropertyChanged();
            }
        }

        private string mobileNbrPrivate;
        public string MobileNbrPrivate
        {
            get { return mobileNbrPrivate; }
            set
            {
                mobileNbrPrivate = value;
                OnPropertyChanged();
            }
        }

        private string bankname;
        public string BankName
        {
            get { return bankname; }
            set
            {
                bankname = value;
                OnPropertyChanged();
            }
        }

        private string bic;
        public string BIC
        {
            get { return bic; }
            set
            {
                bic = value;
                OnPropertyChanged();
            }
        }

        private string iban;
        public string IBAN
        {
            get { return iban; }
            set
            {
                iban = value;
                OnPropertyChanged();
            }
        }

        private string department;
        public string Department
        {
            get { return department; }
            set
            {
                department = value;
                OnPropertyChanged();
            }
        }

        private string manager;
        public string Manager
        {
            get { return manager; }
            set
            {
                manager = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<WarningModel> warningsList = new ObservableCollection<WarningModel>();
        public ObservableCollection<WarningModel> WarningsList
        {
            get { return warningsList; }
            set
            {
                warningsList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<BonusPaymentModel> bonusPaymentList = new ObservableCollection<BonusPaymentModel>();
        public ObservableCollection<BonusPaymentModel> BonusPaymentList
        {
            get { return bonusPaymentList; }
            set
            {
                bonusPaymentList = value;
                OnPropertyChanged();
            }
        }
    }
}
