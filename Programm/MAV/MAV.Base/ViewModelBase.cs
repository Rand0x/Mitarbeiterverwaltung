using System;
using System.Collections.Generic;
using System.Text;

namespace MAV.Base
{
    //Basisklasse für ViewModels um auf Benutzer zuzugreifen
    public class ViewModelBase : PropertyChangedBase
    {
        private UserModel m_User;
        public UserModel User
        {
            get => m_User;
        }

        //Properties um auf wichtige Variablen wie Recht zuzugreifen
        public int UserKey => m_User.UserKey;
        public int? EmployeeKey => m_User.EmployeeKey;
        public int Right => m_User.Right;
        public string RightName => m_User.RightName;

        public ViewModelBase(UserModel user)
        {
            m_User = user;
        }

    }
}
