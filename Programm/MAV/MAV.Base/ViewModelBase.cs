using System;
using System.Collections.Generic;
using System.Text;

namespace MAV.Base
{
    public class ViewModelBase : PropertyChangedBase
    {
        private UserModel m_User;

        public int? EmployeeKey => m_User.EmployeeKey;
        public int Right => m_User.Right;
        public string RightName => m_User.RightName;

        public ViewModelBase(UserModel user)
        {
            m_User = user;
        }

    }
}
