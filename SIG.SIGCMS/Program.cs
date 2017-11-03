using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Fluent;

namespace SIG.SIGCMS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var logger = LogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
            //try
            //{
            //    logger.Debug("init main");
            //    BuildWebHost(args).Run();
            //}
            //catch (Exception e)
            //{
            //    NLog: catch setup errors
            //    logger.Error(e, "Stopped program because of exception");
            //    throw;
            //    }
                  BuildWebHost(args).Run();
            }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseUrls("http://localhost:8000")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();
    }
}
