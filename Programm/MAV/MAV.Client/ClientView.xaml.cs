using MAV.Base;
using MAV.Client.MVVM.View;
using MAV.Client.MVVM.ViewModel;
using ModernWpf.Controls;
using System.Windows;
using System.Windows.Input;

namespace MAV.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ClientView : Window
    {
        private WindowState cacheClientWindowState = WindowState.Normal;

        public ClientView() : this(null) { }

        public ClientView(UserModel user)
        {
            this.DataContext = new ClientViewModel(this, user);
            InitializeComponent();

            if (user.Right > 2)
                AddEmployeeButton.Visibility = Visibility.Hidden;
            if (user.Right > 1)
                AddUserButton.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Die Anwendung wird beendet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowClose(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Fenster wird in der Größe verändert
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowResize(object sender, RoutedEventArgs e)
        {
            // Wenn Client-Fenster nicht Fullscreen ist, wird der aktuelle WindowState (bestehend aus Größe und Position x,y)
            // zwischengespeichert und das Fenster maximiert (Fullscreen). Zusätzlich wird Symbol des Buttons noch geändert
            if (this.WindowState != WindowState.Maximized)
            {
                cacheClientWindowState = this.WindowState;
                this.WindowState = WindowState.Maximized;
                windowButtonResize.Content = "\uE1D8";
                windowButtonResize.ToolTip = "Verkleinern";
            }
            // Das Fenster wird auf zwischengespeicherten WindowState verkleinert und positioniert. Symbol wird wieder zurückgeändert
            else
            {
                this.WindowState = cacheClientWindowState;
                windowButtonResize.Content = "\uE740";
                windowButtonResize.ToolTip = "Vergrößern";
            }
        }

        /// <summary>
        /// Fenster wird minimiert. Noch in der Taskleiste vorhanden
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowMinimize(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Fenster kann bei gedrückter linken Maustaste verschoben werden
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DockPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        /// <summary>
        /// Bei Klick wird ein Dialog geöffnet, bei dem man den Logout nocheinmal bestätigen muss
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnLogoutButton(object sender, RoutedEventArgs e)
        {
            LogoutDialog dialog = new LogoutDialog();
            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                var viewModel = (ClientViewModel)DataContext;
                if (viewModel.LogOutCommand.CanExecute(null))
                    viewModel.LogOutCommand.Execute(null);
            }
        }
    }
}
