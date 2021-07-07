using MAV.Base;

namespace MAV.Client.MVVM.Model
{
    //Model Klasse um Abteilungsdaten aus DB abzuspeichern
    public class DepartmentModel : PropertyChangedBase
    {
        private int key;
        public int Key
        {
            get { return key; }
            set {
                if (value != key)
                {
                    key = value;
                    OnPropertyChanged();
                }
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (value != name)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }

        private string identifier;
        public string Identifier
        {
            get { return identifier; }
            set
            {
                if (value != identifier)
                {
                    identifier = value;
                    OnPropertyChanged();
                }
            }
        }

        private string info;
        public string Info
        {
            get { return info; }
            set
            {
                if (value != info)
                {
                    info = value;
                    OnPropertyChanged();
                }
            }
        }

        private int managerLink;
        public int ManagerLink
        {
            get { return managerLink; }
            set
            {
                if (value != managerLink)
                {
                    managerLink = value;
                    OnPropertyChanged();
                }
            }
        }

        private string managerName;
        public string ManagerName
        {
            get { return managerName; }
            set
            {
                if (value != managerName)
                {
                    managerName = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
