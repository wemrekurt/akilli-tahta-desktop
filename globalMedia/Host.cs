using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.IO;
using System.Windows.Forms;

namespace globalMedia
{
    public class Host
    {
        public string host = "http://";
        private string serialNumber = string.Empty;

        public Host()
        {
            this.host = File.ReadLines("host").First() + "/";
            ManagementObjectSearcher MOS = new ManagementObjectSearcher(" Select * From Win32_BIOS");
            foreach (ManagementObject getserial in MOS.Get())
                this.serialNumber = getserial["SerialNumber"].ToString();
            MessageBox.Show(this.serialNumber);

        }

        private string client(string func)
        {
            try
            {
                using (WebClient wc = new WebClient())          // web istemci oluştur
                {
                    
                    var json = wc.DownloadString(this.host + func + "/" + this.serialNumber + ".json"); // func adresini local server ve seri numara ile birleştirip 
                                                                                                        // oluşturulan yeni adrese istek at ve gelen yanıtı kaydet
                    
                    return json;                                                                        // kaydedilen yanıtı geri döndür
                }
            }
            catch
            {
                return "{\"id\": 5,\"name\": \"none\",\"spec_name\": \"none\",\"state\": true}";
            }
        }

        public GlobalJson controle()
        {
            var ret = this.client("rooms");

            JavaScriptSerializer js = new JavaScriptSerializer();
            GlobalJson sinif = js.Deserialize<GlobalJson>(ret);

            return sinif;
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
