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
using System.Windows.Markup;

namespace MAV.Client.MVVM.View
{
    /// <summary>
    /// Interaktionslogik für EmployeeEditView.xaml
    /// </summary>
    public partial class EmployeeEditView : UserControl
    {
        public EmployeeEditView(object key, ClientViewModel clientVM)
        {
            this.Language = XmlLanguage.GetLanguage("de-DE");
            this.DataContext = new EmployeeEditViewModel(int.Parse(key.ToString()), this, clientVM);
            InitializeComponent();
            cbxSex.ItemsSource = Enum.GetValues(typeof(Sex));
        }
    }
}
