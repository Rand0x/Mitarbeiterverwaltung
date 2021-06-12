using System;
using System.Collections.Generic;
using System.Text;
using MAV.Helper;
using MAV.Base;
using MAV.Client.MVVM.View;

namespace MAV.Client.MVVM.ViewModel
{
    class AddUserViewModel : PropertyChangedBase
    {
        #region Properties
        private AddUserView m_Control;
        public AddUserView Control
        {
            get { return m_Control; }
            private set
            {
                if (value != m_Control)
                {
                    m_Control = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Constructor

        public AddUserViewModel(AddUserView control)
        {
            Control = control;
            CreateCommands();
        }

        #endregion


        #region Commands

        public RelayCommand AddUserCommand { get; private set; }

        private void CreateCommands()
        {
            AddUserCommand = new RelayCommand(AddUser);

        }

        #endregion



        private void AddUser(object parameter = null)
        {
            if (Control.LastName.Text.Length <= 0)
            {
                //ToDo: Meldung, dass Name leer ist
                AddUserDialog addUserDialog = new AddUserDialog();
                addUserDialog.Title = "Kein Nachname";
                addUserDialog.DialogLine1.Text = "Es wurde kein Nachname eingegeben.";
                var result = addUserDialog.ShowAsync();

            }
            else if (Control.FirstName.Text.Length <= 0)
            {
                //ToDo: Meldung, dass Vorname leer ist
                AddUserDialog addUserDialog = new AddUserDialog();
                addUserDialog.Title = "Kein Vorname";
                addUserDialog.DialogLine1.Text = "Es wurde kein Vorname eingegeben.";
                var result = addUserDialog.ShowAsync();
            }
            else if (Control.Password.Password.Length <= 0)
            {
                //ToDo: Meldung, dass kein Passwort gesetzt worden ist
                AddUserDialog addUserDialog = new AddUserDialog();
                addUserDialog.Title = "Kein Passwort";
                addUserDialog.DialogLine1.Text = "Es wurde kein Passwort vergeben.";
                var result = addUserDialog.ShowAsync();

            }
            else if (Control.Password.Password.Length < 8)
            {
                //ToDo: Meldung Passwort ist zu kurz, muss mind. 8 Zeilen groß sein
                AddUserDialog addUserDialog = new AddUserDialog();
                addUserDialog.Title = "Passwort zu kurz";
                addUserDialog.DialogLine1.Text = "Das Passwort muss mindestens ";
                addUserDialog.DialogLine2.Text = "acht Zeichen lang sein.";
                var result = addUserDialog.ShowAsync();
            }
            else if (Control.Password.Password != Control.PasswordRepeat.Password)
            {
                //ToDo: Meldung Passwort und das Passwort zum Wiederholen ist nicht identisch
                AddUserDialog addUserDialog = new AddUserDialog();
                addUserDialog.Title = "Passwort nicht identisch";
                addUserDialog.DialogLine1.Text = "Das Passwort ist nicht mit dem";
                addUserDialog.DialogLine2.Text = "wiederholten Passwort identisch.";
                var result = addUserDialog.ShowAsync();
            }
            else
            {
                //ToDo: Datenbankverbindung um den User zu erstellen!

                bool erfolgreichErstellt = true;

                if (erfolgreichErstellt)
                {
                    AddUserDialog addUserDialog = new AddUserDialog();
                    addUserDialog.Title = "Benutzer erstellt";
                    addUserDialog.DialogLine1.Text = "Es wurde erfolgreich ein Benutzer erstellt!";
                    addUserDialog.DialogLine2.Text = "Benutzername: " + Control.FirstName.Text + "_" + Control.LastName.Text;
                    addUserDialog.DialogLine2.FontSize = 16;
                    ClearAllBoxes();
                    var result = addUserDialog.ShowAsync();
                }
                else
                {
                    AddUserDialog addUserDialog = new AddUserDialog();
                    addUserDialog.Title = "Benutzer nicht erstellt";
                    addUserDialog.DialogLine1.Text = "Benutzer erstellen fehlgeschlagen.";
                    addUserDialog.DialogLine2.Text = "";
                    ClearAllBoxes();
                    var result = addUserDialog.ShowAsync();
                }
            }

        }

        void ClearAllBoxes()
        {
            Control.Password.Password = "";
            Control.PasswordRepeat.Password = "";
            Control.LastName.Text = "";
            Control.FirstName.Text = "";
            Control.Department.Text = "";
        }

    }
}
