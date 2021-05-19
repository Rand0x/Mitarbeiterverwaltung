using System;
using System.Collections.Generic;
using System.Text;

namespace MAV.Base
{
    public class ViewModelBase : PropertyChangedBase
    {
        private UserModel m_User;
        /// <summary>
        /// beinhaltet nKey des Mitarbeiters und zugehöriges Recht zum Abgleich mit benötigten Rechten für best. Funktionen
        /// </summary>
        public UserModel User
        {
            get { return m_User; }
        }

        public ViewModelBase(UserModel user)
        {
            m_User = user;
        }

    }
}
