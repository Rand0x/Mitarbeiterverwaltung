using MAV.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAV.Client.MVVM.Model
{
    public class RightModel : PropertyChangedBase
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
    }
}
