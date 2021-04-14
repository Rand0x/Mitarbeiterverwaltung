using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Bsp
{
  //EventHandler gibt Frontend "bescheid" wenn Properties geändert wurden
  public class OnPropertyChangedBase : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    //CallerMemberName identifiziert den Namen der Property automatisch
    public void OnPropertyChanged([CallerMemberName] string prop = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
  }
}
