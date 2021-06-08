﻿using MAV.DirectoryModule.Model;
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
using ModernWpf.Controls;
using MAV.Client.MVVM.ViewModel;

namespace MAV.Client.MVVM.View
{
    /// <summary>
    /// Interaktionslogik für EmployeeEditView.xaml
    /// </summary>
    public partial class EmployeeEditView : UserControl
    {
        public EmployeeEditView()
        {
            InitializeComponent();

            Department currentDepartments = new Department();
            cmbDepartments.ItemsSource = currentDepartments.Departments;
        }

        private void OnCancelButton(object sender, RoutedEventArgs e)
        {
            var viewModel = (ViewModel.ClientViewModel)DataContext;
            if (viewModel.EmployeeInfoViewCommand.CanExecute(null))
                viewModel.EmployeeInfoViewCommand.Execute(null);
        }

        private void OnSaveButton(object sender, RoutedEventArgs e)
        {

            DirectoryViewModel.UserSelectedEdited.CopyPropsTo(DirectoryViewModel.UserSelected);
            var viewModel = (ViewModel.ClientViewModel)DataContext;
            if (viewModel.EmployeeInfoViewCommand.CanExecute(null))
                viewModel.EmployeeInfoViewCommand.Execute(null);
        }

        private async void OnDeleteEmployeeButton(object sender, RoutedEventArgs e)
        {
            DeleteEmployeeDialog dialog = new DeleteEmployeeDialog();
            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                MessageBox.Show("Hier Prozedur zum Löschen des Mitarbeiters");
            }
        }
    }
}
