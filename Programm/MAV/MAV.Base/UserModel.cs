using MAV.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAV.Base
{
    public class UserModel : PropertyChangedBase
    {

        #region Properties

        private int m_UserKey;
        public int UserKey
        {
            get { return m_UserKey; }
            set {
                if (value != m_UserKey)
                {
                    m_UserKey = value;
                    OnPropertyChanged();
                }
            }
        }


        private int? m_EmployeeKey;
        public int? EmployeeKey
        {
            get { return m_EmployeeKey; }
            private set
            {
                if (value != m_EmployeeKey)
                {
                    m_EmployeeKey = value;
                    OnPropertyChanged();
                }
            }
        }

        private int m_Right;
        public int Right
        {
            get { return m_Right; }
            private set
            {
                if (value != m_Right)
                {
                    m_Right = value;
                    OnPropertyChanged();
                }
            }
        }

        private string m_RightName;
        public string RightName
        {
            get { return m_RightName; }
            private set
            {
                if (value != m_RightName)
                {
                    m_RightName = value;
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
