using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Checker
{
    class Program
    {
        private static globalMedia.Host local = new globalMedia.Host(); // local server ile iletişim başlatıldı
        private static Process proc = new Process();
        private static bool laststate = false;

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;


        static void Main(string[] args)
        {
            var handle = GetConsoleWindow();

            // Hide
            ShowWindow(handle, SW_HIDE);

            // Show
            //ShowWindow(handle, SW_SHOW);

            proc.StartInfo = new ProcessStartInfo("akilli-tahta-desktop.exe");
            proc.StartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;

            System.Timers.Timer timer = new System.Timers.Timer(1000);
            timer.Elapsed += timer_Elapsed;
            timer.Enabled = true;
            proc.Start();

            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
        }

        private static void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            globalMedia.GlobalJson ret = local.controle();
            if (ret.state == true && laststate == false)
            {
                proc.Kill();
            }
            else if (ret.state == false && laststate == true)
            {
                if (!proc.HasExited) proc.Start();
            }


            if (ret.state == false && proc.HasExited != true)
            {
                proc.Start();
            }
            laststate = ret.state;
        }

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    }
}
