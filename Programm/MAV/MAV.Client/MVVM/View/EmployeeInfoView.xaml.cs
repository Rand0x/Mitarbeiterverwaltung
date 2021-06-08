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
        public EmployeeInfoView()
        {
            InitializeComponent();
        }

        private void OnEditButton(object sender, RoutedEventArgs e)
        {
            DirectoryViewModel.UserSelectedEdited = DirectoryViewModel.UserSelected.Clone();
            var viewModel = (ViewModel.ClientViewModel)DataContext;
            if (viewModel.EmployeeEditViewCommand.CanExecute(null))
                viewModel.EmployeeEditViewCommand.Execute(null);
        }
    }
}