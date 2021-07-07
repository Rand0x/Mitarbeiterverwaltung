using MAV.Base;
using MAV.Client.MVVM.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace MAV.Client.MVVM.View
{
    /// <summary>
    /// Interaktionslogik für AddEmployeeView.xaml
    /// </summary>
    public partial class AddEmployeeView : UserControl
    {
        public AddEmployeeView(UserModel user)
        {
            this.Language = XmlLanguage.GetLanguage("de-DE");
            this.DataContext = new AddEmployeeViewModel(this, user);
            InitializeComponent();

            // ComboBox mit Geschlechter aus Enum SexEnum füllen
            cbxSex.ItemsSource = Enum.GetValues(typeof (Gender));

            // Einstellungsdatum ist immer aufs aktuelle Datum voreingestellt
            dpHireDate.SelectedDate = DateTime.Today;            
        }

        /// <summary>
        /// Wenn die Checkbox abgewählt wird, erscheint eine TextBox zum eingeben der gewünschten PersNr
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckboxPersNr_Click(object sender, RoutedEventArgs e)
        {
            if(CheckboxPersNr.IsChecked == false)
            {
                TextBoxPersNr.Visibility = Visibility.Visible;

                // Zu Formatierungszwecken (Abstand zum nächsten Control)
                Thickness margin = CheckboxPersNr.Margin;
                margin.Bottom = 0;
                CheckboxPersNr.Margin = margin;
            }
            else
            {
                TextBoxPersNr.Visibility = Visibility.Collapsed;

                // Zu Formatierungszwecken (Abstand zum nächsten Control)
                Thickness margin = CheckboxPersNr.Margin;
                margin.Bottom = 10;
                CheckboxPersNr.Margin = margin;
            }               
        }

    }
}
