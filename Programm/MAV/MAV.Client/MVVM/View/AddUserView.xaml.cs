using MAV.Base;
using MAV.Client.MVVM.ViewModel;
using System.Windows.Controls;

namespace MAV.Client.MVVM.View
{
    /// <summary>
    /// Interaktionslogik für AdministrationView.xaml
    /// </summary>
    public partial class AddUserView : UserControl
    {
        public AddUserView(UserModel user)
        {
            this.DataContext = new AddUserViewModel(this, user);
            InitializeComponent();
        }
    }
}
