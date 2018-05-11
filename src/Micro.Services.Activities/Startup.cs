using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Base.Commands;
using Micro.Base.Mongo;
using Micro.Base.RabbitMQ;
using Micro.Services.Activities.Domain.Repos;
using Micro.Services.Activities.Handler;
using Micro.Services.Activities.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Micro.Services.Activities
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
            services.AddLogging();
            services.AddMongoDB(Configuration);
            services.AddRabbitMQ(Configuration);
            services.AddSingleton<ICommandHandler<CreateActivity>, CreateActivityHandler>();
            services.AddSingleton<IActivityRepo, ActivityRepo>();
            services.AddSingleton<ICategoryRepo, CategoryRepo>();
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
