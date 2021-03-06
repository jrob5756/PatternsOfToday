﻿using System;
using Factory.Services;
using Factory.Services.Memberships;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Factory
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
            services.AddLogging();

            services
                .AddTransient<IUserService, UserService>()
                .AddTransient<IMembershipFactory, MembershipFactory>()

                .AddTransient<FreeMembership>()
                .AddSingleton<Func<FreeMembership>>(x => x.GetService<FreeMembership>)

                .AddTransient<BronzeMembership>()
                .AddSingleton<Func<BronzeMembership>>(x => x.GetService<BronzeMembership>)

                .AddTransient<SilverMembership>()
                .AddSingleton<Func<SilverMembership>>(x => x.GetService<SilverMembership>)

                .AddTransient<GoldMembership>()
                .AddSingleton<Func<GoldMembership>>(x => x.GetService<GoldMembership>);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
