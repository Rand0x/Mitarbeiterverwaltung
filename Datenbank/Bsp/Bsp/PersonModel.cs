using System;
using System.Collections.Generic;
using System.Text;

namespace Bsp
{
  //speichert Daten für einzelne Personen
  public class PersonModel : OnPropertyChangedBase
  {

    #region Properties

    private int m_ID;
    public int ID
    {
      get { return m_ID; }
      set
      {
        if (value != m_ID)
        {
          m_ID = value;
          OnPropertyChanged();
        }
      }
    }

    private string  m_FirstName;
    public string  FirstName
    {
      get { return m_FirstName; }
      set {
        if (value != m_FirstName)
        {
          m_FirstName = value;
          Changed = true;
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
          Changed = true;
          OnPropertyChanged();
        }
      }
    }

    private bool m_Changed;
    public bool Changed
    {
      get { return m_Changed; }
      set {
        if (value != m_Changed)
        {
          m_Changed = value;
          OnPropertyChanged();
        }
      }
    }

    private bool m_IsNew;
    public bool IsNew
    {
      get { return m_IsNew; }
      set
      {
        if (value != m_IsNew)
        {
          m_IsNew = value;
          OnPropertyChanged();
        }
      }
    }

    #endregion

    public PersonModel(int id, string first, string last, bool isNew = false)
    {
      ID = id;
      FirstName = first;
      LastName = last;
      Changed = false;
      IsNew = isNew;
    }

  }
}
