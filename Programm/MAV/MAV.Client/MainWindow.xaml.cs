using MAV.Client.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace MAV.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WindowState zwischenspeichern = WindowState.Normal;
        public MainWindow()        {

            this.DataContext = new MainViewModel();
            InitializeComponent();
        }

        private void OnWindowClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OnWindowResize(object sender, RoutedEventArgs e)
        {
            if (this.WindowState != WindowState.Maximized)
            {
                zwischenspeichern = this.WindowState;
                this.WindowState = WindowState.Maximized;
                windowButtonResize.Content = "\uE1D8";
                windowButtonResize.ToolTip = "Verkleinern";
            }
            else
            {
                this.WindowState = zwischenspeichern;
                windowButtonResize.Content = "\uE740";
                windowButtonResize.ToolTip = "Vergrößern";
            }
        }

        private void OnWindowMinimize(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void DockPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private async void OnLogoutButton(object sender, RoutedEventArgs e)
        {
            LogoutDialog dialog = new LogoutDialog();
            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                MessageBox.Show($"Hier Logout-Prozedur ausführen");
            }
        }
    }
}
