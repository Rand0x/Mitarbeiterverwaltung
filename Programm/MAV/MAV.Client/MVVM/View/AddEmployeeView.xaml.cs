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
        public AddEmployeeView(UserModel user)
        {
            this.DataContext = new AddEmployeeViewModel(this, user);
            InitializeComponent();
            cbxSex.ItemsSource = Enum.GetValues(typeof (Sex));
            dpHireDate.SelectedDate = DateTime.Today;            
        }

        private void CheckboxPersNr_Click(object sender, RoutedEventArgs e)
        {
            if(CheckboxPersNr.IsChecked == false)
            {
                TextBoxPersNr.Visibility = Visibility.Visible;

                Thickness margin = CheckboxPersNr.Margin;
                margin.Bottom = 0;
                CheckboxPersNr.Margin = margin;
            }
            else
            {
                TextBoxPersNr.Visibility = Visibility.Collapsed;

                Thickness margin = CheckboxPersNr.Margin;
                margin.Bottom = 10;
                CheckboxPersNr.Margin = margin;
            }               
        }

    }
}
