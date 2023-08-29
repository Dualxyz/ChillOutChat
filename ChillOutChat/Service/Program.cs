using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost serviceHost = new ServiceHost(typeof(Service));
            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:10100/Service";


            serviceHost.AddServiceEndpoint(typeof(IService), binding, address);


            Console.WriteLine("ServiceHost opened.");
            serviceHost.Open();
            Console.ReadLine();
            serviceHost.Close();
        }
    }
}
