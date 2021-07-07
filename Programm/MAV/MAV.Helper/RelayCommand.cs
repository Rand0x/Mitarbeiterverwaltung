using System;
using System.Windows.Input;

namespace MAV.Helper
{
  public class RelayCommand : ICommand
  {
    private Action<object> execute;
    private Func<object ,bool> canExecute;

    public RelayCommand(Action<object> ex, Func<object, bool> canex = null)
    {
      execute = ex;
      canExecute = canex;
    }

    public bool CanExecute(object parameter)
    {
      return canExecute is null ? true : canExecute(parameter);
    }

    public void Execute(object parameter)
    {
      execute(parameter);
    }

    public event EventHandler CanExecuteChanged
    {
      add { CommandManager.RequerySuggested += value; }
      remove { CommandManager.RequerySuggested -= value; }
    }
  }
}
