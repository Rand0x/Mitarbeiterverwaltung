namespace MAV.Base
{
    public class UserModel : PropertyChangedBase
    {
        #region Properties

        //nKey des User aus tblUser
        private int userKey;
        public int UserKey
        {
            get { return userKey; }
            set {
                if (value != userKey)
                {
                    userKey = value;
                    OnPropertyChanged();
                }
            }
        }

        //nKey des evtl. Mitarbeiters aus tblEmployee
        private int? employeeKey;
        public int? EmployeeKey
        {
            get { return employeeKey; }
            private set
            {
                if (value != employeeKey)
                {
                    employeeKey = value;
                    OnPropertyChanged();
                }
            }
        }

        //nKey des Rechts aus tblRight
        private int right;
        public int Right
        {
            get { return right; }
            private set
            {
                if (value != right)
                {
                    right = value;
                    OnPropertyChanged();
                }
            }
        }

        //szName des Rechts aus tblRight 
        private string rightName;
        public string RightName
        {
            get { return rightName; }
            private set
            {
                if (value != rightName)
                {
                    rightName = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        public UserModel(int user, int? employee, int right, string rightname)
        {
            UserKey = user;
            EmployeeKey = employee;
            Right = right;
            RightName = rightname;
        }

    }
}
