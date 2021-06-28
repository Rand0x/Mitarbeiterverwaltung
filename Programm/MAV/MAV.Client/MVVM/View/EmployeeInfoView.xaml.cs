using MAV.Base;
using MAV.Client.MVVM.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace MAV.Client.MVVM.View
{
    /// <summary>
    /// Interaktionslogik für EmployeeInfoView.xaml
    /// </summary>
    public partial class EmployeeInfoView : UserControl
    {
        public EmployeeInfoView(object key, UserModel user, ClientViewModel clientVM)
        {
            this.DataContext = new EmployeeInfoViewModel(int.Parse(key.ToString()), clientVM);
            InitializeComponent();

            if (user.Right > 2)
            {
                EditButton.Visibility = Visibility.Hidden;
                sensibleData1.Visibility = Visibility.Hidden;
                sensibleData2.Visibility = Visibility.Hidden;
            }                
        }
    }
}