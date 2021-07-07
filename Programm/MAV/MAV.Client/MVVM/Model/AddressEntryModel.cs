using MAV.Base;

namespace MAV.Client.MVVM.Model
{
    public class AddressEntryModel : PropertyChangedBase  //Model Klasse für Adresslisteneinträge
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
        private int employeenumber;
        public int EmployeeNumber
        {
            get { return employeenumber; }
            set
            {
                employeenumber = value;
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
    }
}
