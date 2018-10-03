﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using BUKMACHER_CORE.Repositories;
using BUKMACHER_INFRASTRUCTURE.IoC.Modules;
using BUKMACHER_INFRASTRUCTURE.Mappers;
using BUKMACHER_INFRASTRUCTURE.Repositories;
using BUKMACHER_INFRASTRUCTURE.Services;
using BUKMACHER_INFRASTRUCTURE.UserRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BUCHMACHER_API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        // data for interface configuration and concrete classes injection.
        public IContainer ApplicationContainer { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        //public void ConfigureServices(IServiceCollection services).
        //IServicePRovider enables us to change ioc container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(AutoMapperConfig.Initialize());
            services.AddScoped<IBukmacherService, BukmacherService>();
            services.AddScoped<IBukmacherRepository, InMemoryBukmacherRepository>();
            services.AddScoped<IUserRepository, InMemoryUserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddMvc();
            
            // Creating new autofac container for better commands handling.
            var builder = new ContainerBuilder();
            builder.RegisterModule<CommandModule>();
            builder.Populate(services);
            ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                    //template: "{controller=User}/{action=Index}/{id?}");
            });
            // Parameter for disposing unsused dependencies.
            appLifetime.ApplicationStopped.Register(() => ApplicationContainer.Dispose());
        }
    }
}
