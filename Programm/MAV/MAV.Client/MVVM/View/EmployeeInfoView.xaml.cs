using MAV.Client.MVVM.ViewModel;
using MAV.DirectoryModule.Model;
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
            var viewModel = (ViewModel.MainViewModel)DataContext;
            if (viewModel.EmployeeEditViewCommand.CanExecute(null))                
                viewModel.EmployeeEditViewCommand.Execute(null);
        }
    }
}
