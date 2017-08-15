using System.IO;
using Microsoft.AspNetCore.Hosting;

using Autofac.Extensions.DependencyInjection;

namespace WebAppAutofacInterception
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()

                .ConfigureServices(services => services.AddAutofac()) // Autofac Method 1 - This line is needed when using Autofac Modules.

                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}