using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Base.Auth;
using Micro.Base.Commands;
using Micro.Base.Mongo;
using Micro.Base.RabbitMQ;
using Micro.Services.Identity.Domain.Repos;
using Micro.Services.Identity.Handlers;
using Micro.Services.Identity.Repo;
using Micro.Services.Identity.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Micro.Services.Identity
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
            services.AddJwt(Configuration);
            services.AddMongoDB(Configuration);
            services.AddRabbitMQ(Configuration);
            services.AddSingleton<ICommandHandler<CreateUser>, CreateUserHandler>();
            services.AddSingleton<IPswrdEncr, PswrdEncr>();
            services.AddSingleton<IUserRepo, UserRepo>();
            services.AddSingleton<IUserService, UserService>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifeTime,
        ILoggerFactory logFactory)

        {
            logFactory.AddDebug();
            logFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.ApplicationServices.GetService<IDBInit>().InitAsync();

            app.UseMvc();
        }
    }
}
