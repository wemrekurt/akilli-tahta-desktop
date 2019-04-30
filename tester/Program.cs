using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace tester
{
    class Program
    {
        static void Main(string[] args)
        {
            string serialNumber = string.Empty;
            ManagementObjectSearcher MOS = new ManagementObjectSearcher(" Select * From Win32_BIOS ");
            foreach (ManagementObject getserial in MOS.Get())
            {
                serialNumber = getserial["SerialNumber"].ToString();
            }
            Console.WriteLine(serialNumber);
            Console.ReadKey();
        }
    }
}
