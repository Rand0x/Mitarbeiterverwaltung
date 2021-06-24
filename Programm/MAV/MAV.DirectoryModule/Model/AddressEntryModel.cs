using System;
using System.Collections.Generic;
using System.Linq;
using MAV.Base;

namespace MAV.DirectoryModule.Model
{
    public class AddressEntryModel : PropertyChangedBase  // Jetzt noch class User, später die Personen aus Datenbank holen
    {
        private int key;      // PropertyChanged nur für Elemente nötig, die im DirectoryView angezigt werden 
        public int Key
        {
            get { return key; }
            set
            {
                key = value;
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
        //private string email;
        //public string EMail
        //{
        //    get { return email; }
        //    set
        //    {
        //        email = value;
        //        OnPropertyChanged();
        //    }
        //}
        //private string job;
        //public string Job
        //{
        //    get { return job; }
        //    set
        //    {
        //        job = value;
        //        OnPropertyChanged();
        //    }
        //}
        //private DateTime hiredate;
        //public DateTime HireDate
        //{
        //    get { return hiredate; }
        //    set
        //    {
        //        hiredate = value;
        //        OnPropertyChanged();
        //    }
        //}
        //private string manager;
        //public string Manager
        //{
        //    get { return manager; }
        //    set
        //    {
        //        manager = value;
        //        OnPropertyChanged();
        //    }
        //}
        //private string persnr;
        //public string PersNr
        //{
        //    get { return persnr; }
        //    set
        //    {
        //        persnr = value;
        //        OnPropertyChanged();
        //    }
        //}
        //private string mobilenmbprivate;
        //public string MobileNmbPrivate
        //{
        //    get { return mobilenmbprivate; }
        //    set
        //    {
        //        mobilenmbprivate = value;
        //        OnPropertyChanged();
        //    }
        //}
        //private string landlinenmbprivate;
        //public string LandlineNmbPrivate
        //{
        //    get { return landlinenmbprivate; }
        //    set
        //    {
        //        landlinenmbprivate = value;
        //        OnPropertyChanged();
        //    }
        //}
        //private DateTime birthday;
        //public DateTime Birthday
        //{
        //    get { return birthday; }
        //    set
        //    {
        //        birthday = value;
        //        OnPropertyChanged();
        //    }
        //}
        //private string sex;
        //public string Sex
        //{
        //    get { return sex; }
        //    set
        //    {
        //        sex = value;
        //        OnPropertyChanged();
        //    }
        //}


        public void CopyPropsTo(AddressEntryModel user)
        {
            user.FirstName = FirstName;
            user.LastName = LastName;
            user.Department = Department;
            user.MobileNbr = MobileNbr;
            user.LandlineNbr = LandlineNbr;
        }

        public AddressEntryModel Clone()
        {
            return (AddressEntryModel)this.MemberwiseClone();
        }
    }
}
