using System.Windows;
using System.Windows.Input;

namespace MAV.Login
{
    /// <summary>
    /// Interaktionslogik für Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            this.DataContext = new LoginViewModel(this);
            InitializeComponent();
            UsernameBox.Focus();
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
        /// Nach Eingabe von Benutzername und Passwort, kann man einfach "Enter"
        /// drücken und der der Loginvorgang wird gestarted 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                ((LoginViewModel)this.DataContext).LogInCommand.Execute(null);
        }
  }
}
