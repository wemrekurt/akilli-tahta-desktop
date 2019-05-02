using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace AkilliTahta
{
    public partial class mainService : ServiceBase
    {
        public mainService()
        {
            InitializeComponent();
        }

        globalMedia.Host local = new globalMedia.Host(); // local server ile iletişim başlatıldı
        Process proc = new Process();
        bool laststate = false;

        protected override void OnStart(string[] args)
        {
            proc.StartInfo = new ProcessStartInfo("akilli-tahta-desktop.exe");
            proc.StartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;

            System.Timers.Timer timer = new System.Timers.Timer(1000);
            timer.Elapsed += timer_Elapsed;
            timer.Enabled = true;
            proc.Start();
        }
        
        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            globalMedia.GlobalJson ret = local.controle();
            if (ret.state == true && laststate == false)
            {
                proc.Kill();
            }
            else if(ret.state == false && laststate == true)
            { 
                if (!proc.HasExited) proc.Start();
            }


            if (ret.state == false && proc.HasExited != true)
            {
                proc.Start();
            }
            laststate = ret.state;
        }


        protected override void OnStop()
        {

        }

        
    }
}
