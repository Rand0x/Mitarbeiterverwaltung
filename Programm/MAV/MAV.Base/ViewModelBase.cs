namespace MAV.Base
{
    //Basisklasse für ViewModels um auf Benutzer zuzugreifen
    public class ViewModelBase : PropertyChangedBase
    {
        private UserModel user;
        public UserModel User
        {
            get => user;
        }

        //Properties um auf wichtige Variablen wie Recht zuzugreifen
        public int UserKey => user.UserKey;
        public int? EmployeeKey => user.EmployeeKey;
        public int Right => user.Right;
        public string RightName => user.RightName;

        public ViewModelBase(UserModel user)
        {
            this.user = user;
        }

    }
}
