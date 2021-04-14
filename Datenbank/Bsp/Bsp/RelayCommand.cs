using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Bsp
{
  //Zuständig für das Ausführen der Commands
  //führt erst Delegate m_CanExecute aus, ist das Ergebnis true wird Delegate m_Execute ausgeführt
  public class RelayCommand : ICommand
  {
    private Action m_Execute;
    private Func<bool> m_CanExecute;

    public RelayCommand(Action ex, Func<bool> canex = null)
    {
      m_Execute = ex;
      m_CanExecute = canex;
    }

    public bool CanExecute(object parameter)
    {
      if (m_CanExecute is null)
        return true;
      else
        return m_CanExecute();
    }

    public void Execute(object parameter)
    {
      m_Execute();
    }

    public event EventHandler m_CanExecuteChanged;

    public void RaiseCanPropertyChanged()
    {

    }

    public event EventHandler CanExecuteChanged
    {
      add { CommandManager.RequerySuggested += value; }
      remove { CommandManager.RequerySuggested -= value; }
    }
  }
}
