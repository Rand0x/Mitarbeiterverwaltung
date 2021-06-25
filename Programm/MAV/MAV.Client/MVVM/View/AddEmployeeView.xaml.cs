using System;
using MAV.Base;
using MAV.Client.MVVM.ViewModel;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MAV.Client.MVVM.View
{
    /// <summary>
    /// Interaktionslogik für AddEmployeeView.xaml
    /// </summary>
    public partial class AddEmployeeView : UserControl
    {
        public AddEmployeeView(object key)
        {
            InitializeComponent();
            dpHireDate.SelectedDate = DateTime.Today;
        }
    }
}
