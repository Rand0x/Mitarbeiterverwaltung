using MAV.Base;
using MAV.Client.MVVM.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace MAV.Client.MVVM.View
{
    /// <summary>
    /// Interaktionslogik für EmployeeInfoView.xaml
    /// </summary>
    public partial class EmployeeInfoView : UserControl
    {
        private EmployeeInfoViewModel infoVM;

        public EmployeeInfoView(object key, UserModel user, ClientViewModel clientVM)
        {
            infoVM = new EmployeeInfoViewModel(int.Parse(key.ToString()), clientVM);
            this.DataContext = infoVM;
            InitializeComponent();
            
            // Wenn der angemeldete Benutzer nicht die nötige Berechtigung hat, wird der Button zum Bearbeiten und die
            // Informationsfelder der perönlichen Daten ausgeblendet
            if (user.Right > 2)
            {
                EditButton.Visibility = Visibility.Hidden;
                sensibleData1.Visibility = Visibility.Hidden;
                sensibleData2.Visibility = Visibility.Hidden;
            }

            // Sind keine Abmahnungen, Bonus-Zahlungen oder Kommentar über den Mitarbeiter vorhanden, werden jeweils die Expander hierfür deaktiviert
            if (infoVM.Employee.WarningsList.Count == 0)
                ExpanderWarnings.IsEnabled = false;            

            if (infoVM.Employee.BonusPaymentList.Count == 0)
                ExpanderBonusPayments.IsEnabled = false;            

            if (infoVM.Employee.Comment == string.Empty)
                ExpanderComment.IsEnabled = false;

            // Inhalt der Kommentarbox kann nicht gebunden werden. Daher wird der Inhalt hiermit manuell eingefügt
            commentBox.Document.Blocks.Clear();
            commentBox.Document.Blocks.Add(new Paragraph(new Run(infoVM.Employee.Comment)));                
        }
    }
}