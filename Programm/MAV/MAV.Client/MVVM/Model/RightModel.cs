using MAV.Base;

namespace MAV.Client.MVVM.Model
{
    //Model Klasse um aus DB gelesene Rechte zu speichern
    public class RightModel : PropertyChangedBase
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
    }
}
