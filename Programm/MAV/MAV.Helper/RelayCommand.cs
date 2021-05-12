using System;
using System.Windows.Input;

namespace MAV.Helper
{
  public class RelayCommand : ICommand
  {
    private Action<object> m_Execute;
    private Func<object ,bool> m_CanExecute;

    public RelayCommand(Action<object> ex, Func<object, bool> canex = null)
    {
      m_Execute = ex;
      m_CanExecute = canex;
    }

    public bool CanExecute(object parameter)
    {
      return m_CanExecute is null ? true : m_CanExecute(parameter);
    }

    public void Execute(object parameter)
    {
      m_Execute(parameter);
    }

    public event EventHandler CanExecuteChanged
    {
      add { CommandManager.RequerySuggested += value; }
      remove { CommandManager.RequerySuggested -= value; }
    }
  }
}
