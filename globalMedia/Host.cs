using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace globalMedia
{
    public class Host
    {
        public string host = "http://192.168.43.17/";
        //private string host = "http://globalmedia.local/";
        private string serialNumber = string.Empty;

        public Host()
        {
            ManagementObjectSearcher MOS = new ManagementObjectSearcher(" Select * From Win32_BIOS");
            foreach (ManagementObject getserial in MOS.Get())
                this.serialNumber = getserial["SerialNumber"].ToString();

            //try
            //{
            //    this.client("wakeup");              // bilgisayarın açıldığını server a haber ver
            //}
            //catch {}
        }

        private string client(string func)
        {
            using (WebClient wc = new WebClient())          // web istemci oluştur
            {
                var json = wc.DownloadString(this.host + func + "/" + this.serialNumber + ".json");   // func adresini local server ve seri numara ile birleştirip 
                                                                                            // oluşturulan yeni adrese istek at ve gelen yanıtı kaydet
                return json;                                                                // kaydedilen yanıtı geri döndür
            }
        }

        public GlobalJson controle()
        {
            var ret = this.client("rooms");

            JavaScriptSerializer js = new JavaScriptSerializer();
            GlobalJson sinif = js.Deserialize<GlobalJson>(ret);

            return sinif;
        }

        public string gethtml()
        {
            using (WebClient wc = new WebClient())          // web istemci oluştur
            {
                var json = wc.DownloadString(this.host);        // ana sayfa istendi 
                // oluşturulan yeni adrese istek at ve gelen yanıtı kaydet
                return json;                                                                // kaydedilen yanıtı geri döndür
            }
        }

    }

    public class GlobalJson
    {
        public int id;
        public string name;
        public string spec_name;
        public bool state;
    }
}
