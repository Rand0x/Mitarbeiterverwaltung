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
using System.Windows.Shapes;

namespace Benutzeroberflaeche
{
    /// <summary>
    /// Interaktionslogik für LoginFenster.xaml
    /// </summary>
    public partial class LoginFenster : Window
    {
        public LoginFenster()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string firstBox = BenutzernameBox.Text;
            string secondBox = PasswortBox.Password;
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
