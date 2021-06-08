using MAV.Base;
using MAV.DirectoryModule.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAV.Client.MVVM.ViewModel
{
    public class DirectoryViewModel : PropertyChangedBase
    {
        private static User userSelected = new User();
        public static User UserSelected
        {
            get
            {
                return userSelected;
            }
            set
            {
                userSelected = value;
            }
        }

        private static User userSelectedEdited = new User();
        public static User UserSelectedEdited
        {
            get
            {
                return userSelectedEdited;
            }
            set
            {
                userSelectedEdited = value;
            }
        }
    }
}
