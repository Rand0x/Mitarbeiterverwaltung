using System;
using MAV.Base;
using System.Collections.Generic;
using System.Text;

namespace MAV.Client.MVVM.Model
{
    public class BonusPaymentModel : PropertyChangedBase
    {
        private string reason;
        public string Reason
        {
            get { return reason; }
            set
            {
                reason = value;
                OnPropertyChanged();
            }
        }

        private double amount;
        public double Amount
        {
            get { return amount; }
            set 
            { 
                amount = value;
                OnPropertyChanged();
            }
        }


        private DateTime dateOfPayment;
        public DateTime DateOfPayment
        {
            get { return dateOfPayment; }
            set
            {
                dateOfPayment = value;
                OnPropertyChanged();
            }
        }

        public string DateOfPaymentAsString
        {
            get
            {
                return ($"{DateOfPayment.Day.ToString("D2")}.{DateOfPayment.Month.ToString("D2")}.{DateOfPayment.Year}");
            }
        }

        private string comment;
        public string Comment
        {
            get { return comment; }
            set
            {
                comment = value;
                OnPropertyChanged();
            }
        }
    }
}
