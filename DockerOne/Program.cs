using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DockerOne
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)

                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(options =>
                    {

                        // Get host name
                        String strHostName = Dns.GetHostName();

                        // Find host by name
                        IPHostEntry ipHostEntry = Dns.GetHostEntry(strHostName);

                        int _port = 8090;
                        // Enumerate IP addresses
                        foreach (IPAddress ipaddress in ipHostEntry.AddressList)
                        {
                            if (ipaddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                                Console.WriteLine($"Listening on IP {ipaddress.ToString()}");
                                Console.WriteLine($"LIstening port: {_port}");
                        }
                        options.Listen(IPAddress.Any, _port);
                    })
                    .UseStartup<Startup>();
                    //webBuilder.UseStartup<Startup>();
                });


    }
}
// dotnet publish -r win-x64 /p:PublishSingleFile=true