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

        public void OnDebug(string[] args)
        {
            OnStart(args);
        }

        string logFolder = string.Empty, todayLog = string.Empty;
        globalMedia.Host local = new globalMedia.Host(); // local server ile iletişim başlatıldı
        Process proc = new Process();
        bool laststate = false;


        protected override void OnStart(string[] args)
        {
            this.logFolder = AppDomain.CurrentDomain.BaseDirectory + "\\logs";                    // log klasörü belirlendi
            this.todayLog = logFolder + "\\" + DateTime.Now.ToString("ddMMyyyy") + "Logger.log";  // bu günki log dosyası belirlendi

            if (!Directory.Exists(this.logFolder)) Directory.CreateDirectory(this.logFolder);                 //Log klasörü yoksa oluşturuldu

            // clear old log files
            string[] files = Directory.GetFiles(this.logFolder);         // log klasöründeki dosyalar istendi
            foreach (string file in files)
            {
                FileInfo fi = new FileInfo(file);                   // dosya bilgileri istendi
                if (fi.LastAccessTime < DateTime.Now.AddDays(-10))  // 10 günden eski dosyalar seçildi
                    fi.Delete();                                    // seçilen dosyalar silindi
            }

            if (!File.Exists(this.todayLog)) File.Create(this.todayLog);      // bu günki log dosyası henüz oluşturulmadıysa oluşturuldu

            proc.StartInfo.FileName = "akilli-tahta-desktop.exe";

            System.Timers.Timer timer = new System.Timers.Timer(1000);
            timer.Elapsed += timer_Elapsed;
            timer.Enabled = true;
            proc.Start();
        }
        
        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            globalMedia.GlobalJson ret = local.controle();
            File.AppendAllText(this.todayLog, ret.state + Environment.NewLine);
            if (ret.state == true && laststate == false)
            {
                proc.Kill();
            }
            else if(ret.state == false && laststate == true)
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
