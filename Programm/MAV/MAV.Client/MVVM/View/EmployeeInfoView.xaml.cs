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
        public EmployeeInfoView(object key)
        {
            this.DataContext = new EmployeeInfoViewModel(int.Parse(key.ToString()));
            InitializeComponent();
        }

        //private void OnEdit_Click(object sender, RoutedEventArgs e)
        //{
        //    var viewModel = (ViewModel.ClientViewModel)DataContext;
        //    viewModel.EmployeeEditViewCommand.Execute(null);
        //}
    }
}