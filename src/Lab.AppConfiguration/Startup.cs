using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab.AppConfiguration.Application;
using Lab.AppConfiguration.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.FeatureManagement;

namespace Lab.AppConfiguration
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration.GetSection("leckerito:Lab:AppSettings"));
            services.AddFeatureManagement();
            services.AddRazorPages();

            services.AddTransient<IEmailService, EmailService>();

            services.AddTransient<ContactService>();
            services.AddTransient<ContactServiceNew>();


            services.AddTransient<IContactService>(services => {
                var featureManager = (IFeatureManagerSnapshot)services.GetRequiredService(typeof(IFeatureManagerSnapshot));
                var isFeatureEnabled = featureManager.IsEnabledAsync("NewContactForm").GetAwaiter().GetResult();

                if (isFeatureEnabled)
                    return (IContactService)services.GetRequiredService(typeof(ContactServiceNew));

                return (IContactService)services.GetRequiredService(typeof(ContactService));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // For live updates
            app.UseAzureAppConfiguration();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
