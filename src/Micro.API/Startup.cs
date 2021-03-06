﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.API.Handlers;
using Micro.API.Repos;
using Micro.Base.Auth;
using Micro.Base.Events;
using Micro.Base.RabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Micro.API
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
            services.AddMvc();
            services.AddJwt(Configuration);

            services.AddRabbitMQ(Configuration);
            services.AddSingleton<IEventHanler<ActivityCreated>, ActivityCreatedHandler>();
            services.AddSingleton<IEventHanler<UserCreated>,UserCreatedHandler>();
            services.AddSingleton<IActivityRepo, ActivityRepo>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
