using MAV.Base;
using System;

namespace MAV.Client.MVVM.Model
{
    public class WarningModel : PropertyChangedBase
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

        private DateTime issueDate;
        public DateTime IssueDate
        {
            get { return issueDate; }
            set
            {
                issueDate = value;
                OnPropertyChanged();
            }
        }

        // Property zum Binden (OneWay) um Datum im richtigen (europäischen) Format darzustellen
        public string IssueDateAsString
        {
            get
            {
                return ($"{IssueDate.Day.ToString("D2")}.{IssueDate.Month.ToString("D2")}.{IssueDate.Year}");
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
