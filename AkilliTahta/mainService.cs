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
            string logFolder = AppDomain.CurrentDomain.BaseDirectory + "\\logs";
            string todayLog = logFolder + "\\" + DateTime.Now.ToString("ddMMyyyy") + "Logger.log";

            if (!Directory.Exists(logFolder)) Directory.CreateDirectory(logFolder);

            // clear old log files
            string[] files = Directory.GetFiles(logFolder);
            foreach (string file in files)
            {
                FileInfo fi = new FileInfo(file);
                if (fi.LastAccessTime < DateTime.Now.AddDays(-10))
                    fi.Delete();
            }

            if (!File.Exists(todayLog)) File.Create(todayLog);

            string serialNumber = string.Empty;
            ManagementObjectSearcher MOS = new ManagementObjectSearcher(" Select * From Win32_BIOS");
            foreach (ManagementObject getserial in MOS.Get())
            {
                serialNumber = getserial["SerialNumber"].ToString();
            }

            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString("url");
            }
        }

        protected override void OnStop()
        {

        }
    }
}
