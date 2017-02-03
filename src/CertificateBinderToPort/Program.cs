using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace CertificateBinderToPort
{
    class Program
    {
        static void Main(string[] args)
        {
            var thumbprint = new X509Certificate2(@"..\..\..\..\Certificados\MyAdHocTestCert.cer").Thumbprint;

            var startInfo = new ProcessStartInfo("netsh.exe", $"http add sslcert ipport=0.0.0.0:5050 certhash={thumbprint} appid={{{Guid.NewGuid()}}}")
            {
                UseShellExecute = false
            };
            Console.WriteLine("Vinculado o certificado a porta:");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{startInfo.FileName} {startInfo.Arguments}");
            Console.ResetColor();
            Process.Start(startInfo).WaitForExit();
            Console.Write("Tecle <ENTER> para sair.");
            Console.ReadLine();
        }
    }
}
