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
        private EmployeeInfoViewModel InfoVM;

        public EmployeeInfoView(object key, UserModel user, ClientViewModel clientVM)
        {
            InfoVM = new EmployeeInfoViewModel(int.Parse(key.ToString()), clientVM);
            this.DataContext = InfoVM;
            InitializeComponent();
                        
            if (user.Right > 2)
            {
                EditButton.Visibility = Visibility.Hidden;
                sensibleData1.Visibility = Visibility.Hidden;
                sensibleData2.Visibility = Visibility.Hidden;
            }

            if (InfoVM.Employee.WarningsList.Count == 0)
                ExpanderWarnings.IsEnabled = false;            

            if (InfoVM.Employee.BonusPaymentList.Count == 0)
                ExpanderBonusPayments.IsEnabled = false;            

            if (InfoVM.Employee.Comment == string.Empty)
                ExpanderComment.IsEnabled = false;

            commentBox.Document.Blocks.Clear();
            commentBox.Document.Blocks.Add(new Paragraph(new Run(InfoVM.Employee.Comment)));                
        }
    }
}