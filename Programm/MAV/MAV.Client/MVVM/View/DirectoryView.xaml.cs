using MAV.Base;
using MAV.Client.MVVM.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            if (user.Right > 2)
                CtxMenuEditButton.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Sortieren der Adressliste alphabetisch nach Vornamen der Mitarbeiter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSortFirstName(object sender, RoutedEventArgs e)
        {
            ((DirectoryViewModel)this.DataContext).OrderByFirstName();
        }

        /// <summary>
        /// Sortieren der Adressliste alphabetisch nach Nachnamen der Mitarbeiter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSortLastName(object sender, RoutedEventArgs e)
        {
            ((DirectoryViewModel)this.DataContext).OrderByLastName();
        }

        /// <summary>
        /// Sortieren der Adressliste nach Telefonnummern der Mitarbeiter (oberste == kleinste Nummer) 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSortLandlineNbr(object sender, RoutedEventArgs e)
        {
            ((DirectoryViewModel)this.DataContext).OrderByLandlineNbr();
        }

        /// <summary>
        /// Sortieren der Adressliste nach Mitarbeiternummern der Mitarbeiter (oberste == kleinste Nummer) 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSortEmployeeNbr(object sender, RoutedEventArgs e)
        {
            ((DirectoryViewModel)this.DataContext).OrderByEmployeeNbr();
        }

        /// <summary>
        /// Sortieren der Adressliste alphabetisch nach Abteilungen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSortDepartment(object sender, RoutedEventArgs e)
        {
            ((DirectoryViewModel)this.DataContext).OrderByDepartment();
        }

        /// <summary>
        /// Bei Doppelklick wird InfoView (Seite mit Informationen) des Mitarbeiters geöffnet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ((DirectoryViewModel)this.DataContext).EmployeeInfoViewCommand.Execute(null);
        }

        /// <summary>
        /// Bei Änderung der Auswahl einer Abteilung wird die gesamte Adressliste nach Mitarbeitern dieser gefiltert
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbDepartments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((DirectoryViewModel)this.DataContext).LoadAddressListCommand.Execute(null);
        }

        /// <summary>
        /// InfoView (Seite mit Informationen) des Mitarbeiters geöffnet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCtxMenuInfo(object sender, RoutedEventArgs e)
        {
            ((DirectoryViewModel)this.DataContext).EmployeeInfoViewCommand.Execute(null);
        }

        /// <summary>
        /// EditView (Seite zum Bearbeiten) des Mitarbeiters wird geöffnet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCtxMenuEdit(object sender, RoutedEventArgs e)
        {
            ((DirectoryViewModel)this.DataContext).EmployeeEditViewCommand.Execute(null);
        }

        /// <summary>
        /// Bei Eingabe eines Namen (Vor- und/oder Nachname ) wird Adressliste danach gefiltert, d.h. der Mitarbeiter gesucht
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ControlsSearchBox_QuerySubmitted(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            ((DirectoryViewModel)this.DataContext).LoadAddressListCommand.Execute(sender.Text);
        }
    }
}