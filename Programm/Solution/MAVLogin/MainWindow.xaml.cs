﻿using System;
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

namespace MAVLogin
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Login(object sender, RoutedEventArgs e)
        {
            if(BenutzernameBox.Text == "" && PasswortBox.Password == "")
            {
                MessageBox.Show("Benutzername oder Passwort falsch", "Ungültige Eingabe");
            }
            else
            {
                
            }
        }

        private void Button_ForgetPassword(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Bitte kontaktieren Sie Ihren Vorgesetzten.", "Passwort vergessen");
        }
    }
}