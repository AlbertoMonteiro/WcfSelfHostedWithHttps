using System;
using System.Net;
using System.Threading;
using eSocial.ServiceClient.eSocialSoapService;

namespace eSocial.ServiceClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Waiting service start(5 seconds)");
            Thread.Sleep(5000);

            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            var helloWorldServiceClient = new HelloWorldServiceClient();

            Console.WriteLine(helloWorldServiceClient.SayHello("Alberto"));
        }
    }
}
