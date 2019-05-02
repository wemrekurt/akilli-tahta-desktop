using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AkilliTahta
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new mainService() 
            };
            ServiceBase.Run(ServicesToRun);
            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
        }
    }
}
