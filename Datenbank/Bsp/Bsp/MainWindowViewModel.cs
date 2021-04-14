using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Bsp
{
  public class MainWindowViewModel : OnPropertyChangedBase
  {

    #region Properties

    //Liste an Personen die aus DB gelesen werden
    //ObservableCollection ist listenartige Datenstruktur
    private ObservableCollection<PersonModel> m_PersonList;
    public ObservableCollection<PersonModel> PersonList
    {
      get { return m_PersonList; }
      set
      {
        if (value != m_PersonList)
        {
          m_PersonList = value;
          OnPropertyChanged();
        }
      }
    }

    //Ausgewählte Person in ListBox
    private PersonModel m_SelectedPerson;
    public PersonModel SelectedPerson
    {
      get { return m_SelectedPerson; }
      set {
        if (value != m_SelectedPerson)
        {
          m_SelectedPerson = value;
          OnPropertyChanged();
        }
      }
    }


    #endregion

    #region Constructor

    public MainWindowViewModel()
    {
      PersonList = new ObservableCollection<PersonModel>();
      //Commands für Buttons initialisieren
      CreateCommands();
      //(Beispiel-)Daten aus DB laden
      LoadPersons();
    }

    #endregion

    #region Commands

    //Command der neue Person erzeugt
    private RelayCommand m_AddCommand;
    public RelayCommand AddCommand
    {
      get { return m_AddCommand; }
      private set { m_AddCommand = value; }
    }

    //Command der Personen in DB ändert/speichert
    private RelayCommand m_SaveCommand;
    public RelayCommand SaveCommand
    {
      get { return m_SaveCommand; }
      private set { m_SaveCommand = value; }
    }

    //Initialisieren der Commands
    private void CreateCommands()
    {
      AddCommand = new RelayCommand(Add, CanAdd);
      SaveCommand = new RelayCommand(Save, CanSave);
    }

    //hier prüfen ob neue Person erstellt werden kann
    //kann evtl weggelassen werden
    private bool CanAdd()
    {
      return true;
    }

    //erstellen einer neuen Person
    private void Add()
    {
      var newPer = new PersonModel(-1, "Max", "Mustermann", true);
      PersonList.Add(newPer);
      SelectedPerson = newPer;
    }

    //hier prüfen ob Daten gespeichert werden können
    private bool CanSave()
    {
      return true;
    }

    //Daten speichern
    private void Save()
    {
      foreach(var person in PersonList)
      {
        if(person.IsNew)
        {
          var param = new ObservableCollection<SqlParameter>();
          param.Add(new SqlParameter("@szFirstName", person.FirstName));
          param.Add(new SqlParameter("@szLastName", person.LastName));
          DBProvider.ExecProcedure("sp_SavePerson", param);
        }
        else if(person.Changed)
        {
          var param = new ObservableCollection<SqlParameter>();
          param.Add(new SqlParameter("@nID", person.ID));
          param.Add(new SqlParameter("@szFirstName", person.FirstName));
          param.Add(new SqlParameter("@szLastName", person.LastName));
          DBProvider.ExecProcedure("sp_ChangePerson", param);
        }
      }
      LoadPersons();
    }

    #endregion

    #region Implementation

    //Laden von Personen aus DB
    public void LoadPersons()
    {
      PersonList.Clear();
      var persons = DBProvider.ExecProcedure("sp_LoadPersons");
      foreach(DataRow row in persons.Rows)
      {
        var id = (int)row["nID"];
        var first = row["szFirstName"].ToString();
        var last = row["szLastName"].ToString();
        PersonList.Add(new PersonModel(id, first, last));
      }
    }

    #endregion

  }
}
