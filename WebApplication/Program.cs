using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Connections;

namespace WebApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }


        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var hostBuilder = WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseKestrel(AddKestrelServerOptions)
                .CaptureStartupErrors(true);

            return hostBuilder;
        }

        private static void AddKestrelServerOptions(WebHostBuilderContext builderContext, KestrelServerOptions options)
        {
            var tcpListenPort = 9900;
            options.ListenAnyIP(tcpListenPort, builder =>
            {
                builder.UseConnectionHandler<MyEchoConnectionHandler>();
            });

            // Configure Kestrel from appsettings.json.
            var kestrelConfig = builderContext.Configuration.GetSection("Kestrel");
            options.Configure(kestrelConfig);
        
        }
    }
}
