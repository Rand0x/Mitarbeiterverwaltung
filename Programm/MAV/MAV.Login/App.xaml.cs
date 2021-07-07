using MAV.Client;
using Microsoft.Win32;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Threading;

namespace MAV.Login
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
  {
        //Timer für Inaktivität des Users
        private DispatcherTimer timer = new DispatcherTimer();

        /// <summary>
        /// Code von: https://social.msdn.microsoft.com/Forums/en-US/a518ff46-fa61-4b9a-b6ee-1443b17c1c56/wpf-application-automatic-lock-setup-?forum=wpf
        /// Letzer Aufruf: 26.06.2021
        /// </summary>
        /// <param name="lii"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        static extern bool GetLastInputInfo(ref LASTINPUTINFO lii);

        /// <summary>
        /// Code von: https://social.msdn.microsoft.com/Forums/en-US/a518ff46-fa61-4b9a-b6ee-1443b17c1c56/wpf-application-automatic-lock-setup-?forum=wpf
        /// Letzer Aufruf: 26.06.2021
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        struct LASTINPUTINFO
        {
            public static readonly int SizeOf = Marshal.SizeOf(typeof(LASTINPUTINFO));

            [MarshalAs(UnmanagedType.U4)]
            public int cbSize;
            [MarshalAs(UnmanagedType.U4)]
            public int dwTime;
        }

        /// <summary>
        /// Event das zum Start des Programms gefeuert wird
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //Timer zum Auto LogOut
            timer.Interval = new TimeSpan(0, 1, 0); //Tick Event wird jede Minute gefeuert
            timer.Tick += Timer_Tick;
            timer.Start();

            //LogOut bei Sperrung des PCs
            SystemEvents.SessionSwitch += new SessionSwitchEventHandler(SystemEvents_SessionSwitch);
        }

        /// <summary>
        /// Tick-Event des Timers, dass einmal alle 60s ausgeführt wird
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            //nach einer Abwesentheit von 5 Minuten wird der Benutzer ausgelogt
            if (GetLastInputTime() >= 300)
                ManageWindows();
        }

        /// <summary>
        /// gibt die Zeit zurück die seit der letzten User Aktivität vergangen ist
        /// Code von: https://social.msdn.microsoft.com/Forums/en-US/a518ff46-fa61-4b9a-b6ee-1443b17c1c56/wpf-application-automatic-lock-setup-?forum=wpf
        /// Letzer Aufruf: 26.06.2021
        /// </summary>
        /// <returns></returns>
        private static int GetLastInputTime()
        {
            int idleTime = 0;
            LASTINPUTINFO lastInputInfo = new LASTINPUTINFO();
            lastInputInfo.cbSize = Marshal.SizeOf(lastInputInfo);
            lastInputInfo.dwTime = 0;

            int envTicks = (int)Environment.TickCount;

            if (GetLastInputInfo(ref lastInputInfo))
            {
                int lastInputTick = lastInputInfo.dwTime;
                idleTime = envTicks - lastInputTick;
            }

            return ((idleTime > 0) ? (idleTime / 1000) : 0);
        }

        /// <summary>
        /// wenn der PC gesperrt wird, soll automatisch ausgelogt werden
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            if (e.Reason == SessionSwitchReason.SessionLock)
                ManageWindows();
        }

        /// <summary>
        /// Hilfsmethode, die den Client schließt und den Login öffnet
        /// </summary>
        private void ManageWindows()
        {
            foreach (Window win in Application.Current.Windows.OfType<ClientView>())
                win.Close(); //schließen des Clients
            foreach (Window win in Application.Current.Windows.OfType<Login>())
                win.Show(); //anzeigen des Logins
        }
    }
}
