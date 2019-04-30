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

        protected override void OnStart(string[] args)
        {
            string logFolder = AppDomain.CurrentDomain.BaseDirectory + "\\logs";                    // log klasörü belirlendi
            string todayLog = logFolder + "\\" + DateTime.Now.ToString("ddMMyyyy") + "Logger.log";  // bu günki log dosyası belirlendi

            if (!Directory.Exists(logFolder)) Directory.CreateDirectory(logFolder);                 //Log klasörü yoksa oluşturuldu

            // clear old log files
            string[] files = Directory.GetFiles(logFolder);         // log klasöründeki dosyalar istendi
            foreach (string file in files)
            {
                FileInfo fi = new FileInfo(file);                   // dosya bilgileri istendi
                if (fi.LastAccessTime < DateTime.Now.AddDays(-10))  // 10 günden eski dosyalar seçildi
                    fi.Delete();                                    // seçilen dosyalar silindi
            }

            if (!File.Exists(todayLog)) File.Create(todayLog);      // bu günki log dosyası henüz oluşturulmadıysa oluşturuldu

            globalMedia.Host local = new globalMedia.Host();        // local server ile iletişim başlatıldı

            

        }

        protected override void OnStop()
        {

        }
    }
}
