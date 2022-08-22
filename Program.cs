using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceProcess;
using Microsoft.Win32;
using Utilities;

namespace UnquotedServicePath
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceController[] services = ServiceController.GetServices();

            foreach (ServiceController svcs in services)
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SYSTEM\\CurrentControlSet\\Services\\" + svcs.ServiceName);
                string path = key.GetValue("ImagePath").ToString();

                if (path[0] != '"' && !path.Contains("system32") && !path.Contains("System32"))
                {
//                     https://stackoverflow.com/a/54943087
                    var t = new TablePrinter("Service Name", "Service Path");
                    t.AddRow(svcs.ServiceName, path);
                    t.Print();
                }
            }
        }
    }
}
