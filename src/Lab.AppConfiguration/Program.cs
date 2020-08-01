using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

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
                        // hostingContext.HostingEnvironment.IsProduction()

                        var settings = config.Build();
                        //var azureAppConfigConnectionString = settings["ConnectionStrings:AppConfig"];

                        var clientId = settings["Azure:LeckeritoServicePrinzipal:ClientId"];
                        var clientSecret = settings["Azure:LeckeritoServicePrinzipal:ClientSecret"];
                        var tenantId = settings["Azure:LeckeritoServicePrinzipal:TenantId"];

                        var credentials = new ClientSecretCredential(tenantId, clientId, clientSecret);

                        config.AddAzureAppConfiguration(options => {
                            options.Connect(new Uri("https://leckeritoappsettings.azconfig.io/"), credentials)
                            // Connect with ConnectionString from User Secrets
                            //options.Connect(azureAppConfigConnectionString)
                                    // For live updates
                                    .ConfigureRefresh(refresh => 
                                    {
                                        refresh.Register("leckerito:Lab:AppSettings:ReloadSentinel", refreshAll: true)
                                                .SetCacheExpiration(new TimeSpan(0, 0, 3));
                                    })
                                    .ConfigureKeyVault(kv => {
                                        kv.SetCredential(credentials);
                                    })
                                    .Select("leckerito:Lab:AppSettings:*", LabelFilter.Null)
                                    // Override with any configuration values specific to current hosting env
                                    .Select("leckerito:Lab:AppSettings:*", hostingContext.HostingEnvironment.EnvironmentName);
                        });
                    });

                   

                    webBuilder.UseStartup<Startup>();
                });
    }
}
