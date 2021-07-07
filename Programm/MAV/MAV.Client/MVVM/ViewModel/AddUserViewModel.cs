using MAV.Base;
using MAV.Client.MVVM.Model;
using MAV.Client.MVVM.View;
using MAV.Helper;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace MAV.Client.MVVM.ViewModel
{
    class AddUserViewModel : ViewModelBase
    {
        #region Properties

        private AddUserView control;
        //zugehöriges View
        public AddUserView Control
        {
            get { return control; }
            private set
            {
                if (value != control)
                {
                    control = value;
                    OnPropertyChanged();
                }
            }
        }

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (value != firstName)
                {
                    firstName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                if (value != lastName)
                {
                    lastName = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<RightModel> rights;
        //Liste aller vorhandenen Rechte aus DB
        public ObservableCollection<RightModel> Rights
        {
            get { return rights; }
            set
            {
                if (value != rights)
                {
                    rights = value;
                    OnPropertyChanged();
                }
            }
        }

        private RightModel selectedRight;
        public RightModel SelectedRight
        {
            get { return selectedRight; }
            set
            {
                if (value != selectedRight)
                {
                    selectedRight = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Constructor

        public AddUserViewModel(AddUserView control, UserModel user) : base(user)
        {
            Control = control;

            FirstName = string.Empty;
            LastName = string.Empty;

            Rights = new ObservableCollection<RightModel>();
            SelectedRight = null;
            LoadRights();

            CreateCommands();
        }

        #endregion

        #region Commands

        //Command um User hinzuzufügen
        public RelayCommand AddUserCommand { get; private set; }

        private void CreateCommands()
        {
            AddUserCommand = new RelayCommand(AddUser);
        }

        #endregion

        #region Implementation

        /// <summary>
        /// Wenn alle benötigten Daten eingegeben wurden, wird ein neuer Benutzer (mit entsprechendem Recht) zum Anmelden erstellt 
        /// </summary>
        /// <param name="parameter"></param>
        private void AddUser(object parameter = null)
        {
            //Überprüfen der eingegebenen Daten
            if (LastName is null) //Nachname vorhanden?
            {
                DialogPopUp("Kein Nachname", "Es wurde kein Nachname eingegeben");
            }
            else if (FirstName is null) //Vorname vorhanden?
            {
                DialogPopUp("Kein Vorname", "Es wurde kein Vorname eingegeben");
            }
            else if (Control.Password.Password is null) //Passwort vorhanden?
            {
                DialogPopUp("Kein Passwort", "Es wurde kein Passwort eingegeben");
            }
            else if (Control.Password.Password.Length < 8) //Passwort lang genut?
            {
                DialogPopUp("Passwort ist zu kurz", "Das Passwort muss mindestens", "acht Zeichen lang sein.");
            }
            else if (Control.Password.Password != Control.PasswordRepeat.Password) //Passwort bestätigt?
            {
                DialogPopUp("Passwörter nicht identisch", "Das Passwort ist nicht mit dem", "wiederholten Passwort identisch.");
            }
            else if (SelectedRight is null) //Recht ausgewählt?
            {
                DialogPopUp("Kein Recht vergeben", "Die erstellte Person braucht Rechte");
            }
            else
            {
                //sp Parameter für Aufruf zusammenstellen
                var param = new ObservableCollection<SqlParameter>();

                param.Add(new SqlParameter("@szFirstName", FirstName));
                param.Add(new SqlParameter("@szLastName", LastName));

                //hashen des Passworts damit kein Klartext in DB abgespeichert wird
                var hash = PasswordHash.Hash(Control.Password.Password, out var salt);

                param.Add(new SqlParameter("@szPassword", ByteStringConverter.ToStringFromBytes(hash)));
                param.Add(new SqlParameter("@szSalt", ByteStringConverter.ToStringFromBytes(salt)));
                param.Add(new SqlParameter("@nRightLink", SelectedRight.Key));

                try
                {
                    DBProvider.ExecProcedure("sp_CreateUser", param); //anlegen des neuen Users
                }
                catch (Exception ex)
                {
                    DialogPopUp("Fehler", ex.Message);
                    ClearBoxes();
                    return;
                }
                DialogPopUp("Erfolgreich erstellt", "Der Benutzer wurde erfolgreich erstellt", $"Benutzername: {Control.FirstName.Text}_{Control.LastName.Text}");
                ClearBoxes();
            }
        }

        /// <summary>
        /// Lädt alle vorhandenen Rechte aus der Datenbank
        /// </summary>
        private void LoadRights()
        {
            DataTable result;

            try
            {
                result = DBProvider.ExecProcedure("sp_LoadRights");
            }
            catch (Exception ex)
            {
                DialogPopUp("Fehler", ex.Message);
                return;
            }

            //geladene Rechte zu Liste hinzufügen
            foreach (DataRow row in result.Rows)
            {
                Rights.Add(new RightModel()
                {
                    Key = (int)row["nKey"],
                    Name = row["szName"].ToString(),
                    Info = row["szInfo"].ToString()
                });
            }
        }

        /// <summary>
        /// Alle Felder zur Eingabe von Daten werden wieder auf Startzustand zurückgesetzt
        /// </summary>
        private void ClearBoxes()
        {
            FirstName = null;
            LastName = null;
            Control.Password.Clear();
            Control.PasswordRepeat.Clear();
            SelectedRight = null;
        }

        /// <summary>
        /// Beim Aufrufen erscheint ein Dialog-Fenster
        /// </summary>
        /// <param name="title">Title des Fensters</param>
        /// <param name="firstLine">Erste Zeile des Fensters</param>
        /// <param name="secondLine">Zweite Zeile des Fensters</param>
        private void DialogPopUp(string title, string firstLine, string secondLine = "")
        {
            Dialog dialog = new Dialog();
            dialog.Title = title;
            dialog.FirstLineContent = firstLine;
            dialog.SecondLineText = secondLine;
            var result = dialog.ShowAsync();
        }

        #endregion
    }
}
