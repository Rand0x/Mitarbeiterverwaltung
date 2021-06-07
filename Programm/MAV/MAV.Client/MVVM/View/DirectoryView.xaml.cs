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
    /// Interaktionslogik für DirectoryView.xaml
    /// </summary>
    public enum AutoSuggestionBoxTextChangeReason
    {
        UserInput = 0,
        ProgrammaticChange = 1,
        SuggestionChosen = 2
    }
    /// <summary>
    /// Interaktionslogik für DirectoryView.xaml
    /// </summary>
    public partial class DirectoryView : UserControl
    {
        Person allUsers = new Person();

        private string selectedDepartment = Department.TermForAllDeps;

        private static User userSelected = new User();
        public static User UserSelected { get {return userSelected; } }

        public DirectoryView()
        {
            InitializeComponent();

            // Als Inhalt der Combobox werden alle vorhandenen Abteilungen abgefragt + eine Bezeichnung 
            // für alle Abteilungen (als Default-Wert ausgewählt)
            Department currentDepartments = new Department();
            List<string> departments = currentDepartments.Departments;
            departments.Add(Department.TermForAllDeps);
            cmbDepartments.ItemsSource = departments;  
            cmbDepartments.SelectedItem = Department.TermForAllDeps;

            lvUsers.ItemsSource = allUsers.OrderByFirstName(); 
        }

        private void sortFirstName(object sender, RoutedEventArgs e)
        {
            lvUsers.ItemsSource = allUsers.OrderByFirstName();
        }

        private void sortLastName(object sender, RoutedEventArgs e)
        {
            lvUsers.ItemsSource = allUsers.OrderByLastName();
        }

        private void sortLandlineNbr(object sender, RoutedEventArgs e)
        {
            lvUsers.ItemsSource = allUsers.OrderByLandlineNbr();
        }

        private void sortMobileNbr(object sender, RoutedEventArgs e)
        {
            lvUsers.ItemsSource = allUsers.OrderByMobileNbr();
        }

        private void sortDepartment(object sender, RoutedEventArgs e)
        {
            lvUsers.ItemsSource = allUsers.OrderByDepartment();
        }

        void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DirectoryViewModel.UserSelected = lvUsers.SelectedItem as User;

            if (userSelected != null)
            {
                var viewModel = (ViewModel.MainViewModel)DataContext;
                if (viewModel.EmployeeInfoViewCommand.CanExecute(null))
                    viewModel.EmployeeInfoViewCommand.Execute(null);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DirectoryViewModel.UserSelected = lvUsers.SelectedItem as User;
            var viewModel = (ViewModel.MainViewModel)DataContext;
            if (viewModel.EmployeeInfoViewCommand.CanExecute(null))
                viewModel.EmployeeInfoViewCommand.Execute(null);
        }

        private void cmbDepartments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object selectedItem = cmbDepartments.SelectedItem;
            selectedDepartment = selectedItem.ToString();
            allUsers.FilterByDepartments(selectedDepartment);
            lvUsers.ItemsSource = allUsers.OrderByFirstName();
        }

        private void OnCtxMenuInfo(object sender, RoutedEventArgs e)
        {
            DirectoryViewModel.UserSelected = lvUsers.SelectedItem as User;
            if (userSelected != null)
            {
                var viewModel = (ViewModel.MainViewModel)DataContext;
                if (viewModel.EmployeeInfoViewCommand.CanExecute(null))
                    viewModel.EmployeeInfoViewCommand.Execute(null);
            }
        }

        private void OnCtxMenuEdit(object sender, RoutedEventArgs e)
        {
            User userSelected = lvUsers.SelectedItem as User;
            DirectoryViewModel.UserSelectedEdited = userSelected.Clone();
            if (userSelected != null)
            {
                var viewModel = (ViewModel.MainViewModel)DataContext;
                if (viewModel.EmployeeEditViewCommand.CanExecute(null))
                    viewModel.EmployeeEditViewCommand.Execute(null);
            }
        }

        private void controlsSearchBox_QuerySubmitted(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            string input = sender.Text.ToLower();
            lvUsers.ItemsSource = allUsers.SearchUser(input);
        }
    }    
}
