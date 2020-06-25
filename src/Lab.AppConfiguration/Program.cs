using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;

namespace Lab.AppConfiguration
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
                    webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        var settings = config.Build();
                        config.AddAzureAppConfiguration(options => {
                            options.Connect(settings["ConnectionStrings:AppConfig"])
                                    .ConfigureRefresh(refresh => 
                                    {
                                        refresh.Register("leckerito:Lab:AppSettings:ReloadSentinel", refreshAll: true)
                                                .SetCacheExpiration(new TimeSpan(0, 0, 3));
                                    });
                        });
                    });

                   

                    webBuilder.UseStartup<Startup>();
                });
    }
}
