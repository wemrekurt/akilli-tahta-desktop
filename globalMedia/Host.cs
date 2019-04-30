using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace globalMedia
{
    public class Host
    {
        private string host = "http://globalmedia.local/";
        private string serialNumber = string.Empty;

        public Host()
        {
            ManagementObjectSearcher MOS = new ManagementObjectSearcher(" Select * From Win32_BIOS");
            foreach (ManagementObject getserial in MOS.Get())
                this.serialNumber = getserial["SerialNumber"].ToString();

            try
            {
                this.client("wakeup");              // bilgisayarın açıldığını server a haber ver
            }
            catch {}
        }

        private string client(string func)
        {
            using (WebClient wc = new WebClient())          // web istemci oluştur
            {
                var json = wc.DownloadString(this.host + func + "/" + this.serialNumber);   // func adresini local server ve seri numara ile birleştirip 
                                                                                            // oluşturulan yeni adrese istek at ve gelen yanıtı kaydet
                return json;                                                                // kaydedilen yanıtı geri döndür
            }
        }

        private string filter(string tt)
        {
            return tt.Replace(" ", string.Empty).ToLower();     //tt metnini filreden geçir (tüm boşlukları sil ve tüm harfleri küçült)
        }

        public bool checkState()
        {
            try
            {
                string data = this.filter(this.client("state"));    // state bilgisini istemciden talep et ve filtreden geçirip data değişkenine aktar
                if(String.Equals(data,"true"))  // eğer state değeri true ise;
                    return true;                // true değerini geri döndür
                else
                    return false;               // false değerini geri döndür
            }
            catch
            {
                return false;
            }
        }

        public void shutdown()
        {
            try
            {
                this.filter(this.client("shutdown"));    // sistemin kapatıldığını haber ver
            }
            catch { }
        }

        public void add()
        {
            try
            {
                this.filter(this.client("add"));    // yeni bilgisayarın eklenmesi için haber ver
            }
            catch { }
        }

        public string board()
        {
            try
            {
                string ret = this.filter(this.client("board"));    // ilişkili olduğu sınıfın bilgilerini al
                return ret;
            }
            catch 
            { 
                return "<h2>Bir iletişim hatası oluştu.</h2>";
            }
        }

    }
}
