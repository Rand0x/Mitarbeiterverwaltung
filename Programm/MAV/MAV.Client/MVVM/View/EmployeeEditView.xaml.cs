using MAV.Client.MVVM.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace MAV.Client.MVVM.View
{
    /// <summary>
    /// Interaktionslogik für EmployeeEditView.xaml
    /// </summary>
    public partial class EmployeeEditView : UserControl
    {
        private EmployeeEditViewModel editVM;

        public EmployeeEditView(object key, ClientViewModel clientVM)
        {
            this.Language = XmlLanguage.GetLanguage("de-DE");
            editVM = new EmployeeEditViewModel(int.Parse(key.ToString()), this, clientVM);
            this.DataContext = editVM;
            InitializeComponent();
            
            // Inhalt des Kommentarfelds laden
            editVM.UpdateRichTextBoxContent();

            //ComboBox mit Geschlechter aus Enum SexEnum füllen
            cbxSex.ItemsSource = Enum.GetValues(typeof(Gender));
        }

        /// <summary>
        /// Methode wird aufgerufen, welche ein leeres Feld (Vorlage) erzeugt, um neue Bonus-Zahlung einzugeben 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAddBonusPayment(object sender, RoutedEventArgs e)
        {
            editVM.AddFieldForBonusPayment();
        }

        /// <summary>
        /// Methode wird aufgerufen, welche ein leeres Feld (Vorlage) erzeugt, um neue Abmahnung einzugeben 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAddWarning(object sender, RoutedEventArgs e)
        {
            editVM.AddFieldForWarning();
        }
    }
}
