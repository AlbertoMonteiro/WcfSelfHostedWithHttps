using System;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace eSocial.ServiceMock
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const string serviceAddress = "https://127.0.0.1:5050/WcfService";
            var binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);

            var serviceHost = new ServiceHost(typeof(HelloWorldService), new Uri(serviceAddress));
            serviceHost.AddServiceEndpoint(typeof(IHelloWorldService), binding, "");
            serviceHost.Credentials.ServiceCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindBySubjectName, "ALBERTO-PC");

            var smb = new ServiceMetadataBehavior
            {
                HttpsGetEnabled = true,
                MetadataExporter = { PolicyVersion = PolicyVersion.Policy15 }
            };
            serviceHost.Description.Behaviors.Add(smb);

            try
            {
                serviceHost.Open();

                Console.WriteLine("Escutando no endereço @ {0}", serviceAddress);
                Console.WriteLine("Press enter to close the service");
                Console.ReadLine();
                serviceHost.Close();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("A commmunication error occurred: {0}", ce.Message);
                Console.WriteLine();
            }
            catch (Exception exc)
            {
                Console.WriteLine("An unforseen error occurred: {0}", exc.Message);
                Console.ReadLine();
            }
        }
    }

    [ServiceContract]
    public interface IHelloWorldService
    {
        [OperationContract]
        string SayHello(string name);
    }

    public class HelloWorldService : IHelloWorldService
    {
        public string SayHello(string name) => $"Hello, {name}";
    }
}