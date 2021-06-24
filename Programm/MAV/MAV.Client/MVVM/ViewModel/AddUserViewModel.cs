using System;
using System.Collections.Generic;
using System.Text;
using MAV.Helper;
using MAV.Base;
using MAV.Client.MVVM.View;
using System.Security.Cryptography;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using MAV.Client.MVVM.Model;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MAV.Client.MVVM.ViewModel
{
    class AddUserViewModel : ViewModelBase
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

        private string m_FirstName;
        public string FirstName
        {
            get { return m_FirstName; }
            set
            {
                if (value != m_FirstName)
                {
                    m_FirstName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string m_LastName;
        public string LastName
        {
            get { return m_LastName; }
            set
            {
                if (value != m_LastName)
                {
                    m_LastName = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<RightModel> m_Rights;
        public ObservableCollection<RightModel> Rights
        {
            get { return m_Rights; }
            set
            {
                if (value != m_Rights)
                {
                    m_Rights = value;
                    OnPropertyChanged();
                }
            }
        }

        private RightModel m_SelectedRight;
        public RightModel SelectedRight
        {
            get { return m_SelectedRight; }
            set
            {
                if (value != m_SelectedRight)
                {
                    m_SelectedRight = value;
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

        public RelayCommand AddUserCommand { get; private set; }

        private void CreateCommands()
        {
            AddUserCommand = new RelayCommand(AddUser);
        }

        #endregion

        private void AddUser(object parameter = null)
        {
            if (LastName is null)
            {
                DialogPopUp("Kein Nachname", "Es wurde kein Nachname eingegeben");
            }
            else if (FirstName is null)
            {
                DialogPopUp("Kein Vorname", "Es wurde kein Vorname eingegeben");
            }
            else if (Control.Password.Password is null)
            {
                DialogPopUp("Kein Passwort", "Es wurde kein Passwort eingegeben");
            }
            else if (Control.Password.Password.Length < 8)
            {
                DialogPopUp("Passwort ist zu kurz", "Das Passwort muss mindestens", "acht Zeichen lang sein.");
            }
            else if (Control.Password.Password != Control.PasswordRepeat.Password)
            {
                DialogPopUp("Passwörter nicht identisch", "Das Passwort ist nicht mit dem", "wiederholten Passwort identisch.");
            }
            else if (SelectedRight is null)
            {
                DialogPopUp("Kein Recht vergeben", "Die erstellte Person braucht Rechte");
            }
            else
            {
                var param = new ObservableCollection<SqlParameter>();

                param.Add(new SqlParameter("@szFirstName", FirstName));
                param.Add(new SqlParameter("@szLastName", LastName));

                var hash = PasswordHash.Hash(Control.Password.Password, out var salt);

                param.Add(new SqlParameter("@szPassword", ByteStringConverter.ToStringFromBytes(hash)));
                param.Add(new SqlParameter("@szSalt", ByteStringConverter.ToStringFromBytes(salt)));
                param.Add(new SqlParameter("@nRightLink", SelectedRight.Key));

                try
                {
                    DBProvider.ExecProcedure("sp_CreateUser", param);
                }
                catch(Exception ex)
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
            catch(Exception ex)
            {
                DialogPopUp("Fehler", ex.Message);
                return;
            }

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
        /// Leert die Boxen
        /// </summary>
        void ClearBoxes()
        {
            FirstName = null;
            LastName = null;
            Control.Password.Clear();
            Control.PasswordRepeat.Clear();
            SelectedRight = null;
        }

        /// <summary>
        /// Beim Aufrufen erscheint ein Fenster
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

    }
}
