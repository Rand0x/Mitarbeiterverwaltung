using MAV.Base;
using MAV.Client.MVVM.ViewModel;
using System.Windows.Controls;

namespace MAV.Client.MVVM.View
{
    /// <summary>
    /// Interaktionslogik für SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        public SettingsView(UserModel user)
        {
            this.DataContext = new SettingsViewModel(user, this);
            InitializeComponent();
        }
    }
}
