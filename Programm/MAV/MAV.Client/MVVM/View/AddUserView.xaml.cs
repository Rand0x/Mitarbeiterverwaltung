using System;
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
using MAV.Client.MVVM.ViewModel;
using MAV.DirectoryModule.Model;

namespace MAV.Client.MVVM.View
{
    /// <summary>
    /// Interaktionslogik für AdministrationView.xaml
    /// </summary>
    public partial class AddUserView : UserControl
    {
        public AddUserView()
        {
            InitializeComponent();
            this.DataContext = new AddUserViewModel(this);


            Right currentRight = new Right();
            List<string> rights = currentRight.Rights;
            cmbRights.ItemsSource = rights;
            cmbRights.SelectedItem = rights[2];
        }
    }
}
