using MAV.Base;
using System.Collections.Generic;

namespace MAV.Client.MVVM.Model
{
    public class DepartmentModel : PropertyChangedBase
    {
        private int m_Key;
        public int Key
        {
            get { return m_Key; }
            set {
                if (value != m_Key)
                {
                    m_Key = value;
                    OnPropertyChanged();
                }
            }
        }

        private string m_Name;
        public string Name
        {
            get { return m_Name; }
            set
            {
                if (value != m_Name)
                {
                    m_Name = value;
                    OnPropertyChanged();
                }
            }
        }

        private string m_Identifier;
        public string Identifier
        {
            get { return m_Identifier; }
            set
            {
                if (value != m_Identifier)
                {
                    m_Identifier = value;
                    OnPropertyChanged();
                }
            }
        }

        private string m_Info;
        public string Info
        {
            get { return m_Info; }
            set
            {
                if (value != m_Info)
                {
                    m_Info = value;
                    OnPropertyChanged();
                }
            }
        }

        private int m_ManagerLink;
        public int ManagerLink
        {
            get { return m_ManagerLink; }
            set
            {
                if (value != m_ManagerLink)
                {
                    m_ManagerLink = value;
                    OnPropertyChanged();
                }
            }
        }

        private string m_ManagerName;
        public string ManagerName
        {
            get { return m_ManagerName; }
            set
            {
                if (value != m_ManagerName)
                {
                    m_ManagerName = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
