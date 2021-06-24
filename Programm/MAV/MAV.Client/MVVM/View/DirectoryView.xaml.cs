using System;
using System.Collections.Generic;
using MAV.Client.MVVM.ViewModel;
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
using MAV.Base;
using MAV.DirectoryModule.Model;

namespace MAV.Client.MVVM.View
{
    /// <summary>
    /// Interaktionslogik für DirectoryView.xaml
    /// </summary>
    public partial class DirectoryView : UserControl
    {
        public DirectoryView(UserModel user, ClientViewModel vm)
        {
            this.DataContext = new DirectoryViewModel(user, vm);
            InitializeComponent();
        }

        private void sortFirstName(object sender, RoutedEventArgs e)
        {
            ((DirectoryViewModel)this.DataContext).OrderByFirstName();
        }

        private void sortLastName(object sender, RoutedEventArgs e)
        {
            ((DirectoryViewModel)this.DataContext).OrderByLastName();
        }

        private void sortLandlineNbr(object sender, RoutedEventArgs e)
        {
            ((DirectoryViewModel)this.DataContext).OrderByLandlineNbr();
        }

        private void sortMobileNbr(object sender, RoutedEventArgs e)
        {
            ((DirectoryViewModel)this.DataContext).OrderByMobileNbr();
        }

        private void sortDepartment(object sender, RoutedEventArgs e)
        {
            ((DirectoryViewModel)this.DataContext).OrderByDepartment();
        }

        void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ((DirectoryViewModel)this.DataContext).EmployeeInfoViewCommand.Execute(null);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ((DirectoryViewModel)this.DataContext).EmployeeInfoViewCommand.Execute(null);
        }

        private void cmbDepartments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((DirectoryViewModel)this.DataContext).LoadAddressListCommand.Execute(null);
        }

        private void OnCtxMenuInfo(object sender, RoutedEventArgs e)
        {
            ((DirectoryViewModel)this.DataContext).EmployeeInfoViewCommand.Execute(null);
        }

        private void OnCtxMenuEdit(object sender, RoutedEventArgs e)
        {
            ((DirectoryViewModel)this.DataContext).EmployeeEditViewCommand.Execute(null);
        }

        private void controlsSearchBox_QuerySubmitted(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            ((DirectoryViewModel)this.DataContext).LoadAddressListCommand.Execute(null);
        }

    }
}