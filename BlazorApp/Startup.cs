using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Services;

namespace BlazorApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddSingleton<IHubService, HubService>();

            services.AddScoped<IAlertService, AlertService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IChatService, ChatService>();


            // Register all types and instances to service container 
            RegisterTypesToServiceContainer(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/Error");
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
                endpoints.MapHub<ChatHub>(ChatHub.HUB_URL);
            });
        }

        public void RegisterTypesToServiceContainer(IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            ServiceContainer serviceContainer = ServiceContainer.Instance;
            foreach (var item in services)
            {
                Type type = item.ServiceType;
                Type[] interfaces = type.GetInterfaces();
                foreach (var @interface in interfaces)
                {
                    if (@interface == typeof(IService))
                    {
                        serviceContainer.AddService(type, sp.GetService(type));
                    }
                }
            }
        }
    }
}
