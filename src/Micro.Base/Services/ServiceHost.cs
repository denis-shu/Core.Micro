using System;
using Micro.Base.Commands;
using Micro.Base.Events;
using Micro.Base.RabbitMQ;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using RawRabbit;

namespace Micro.Base.Services
{
    public class ServiceHost : IServiceHost
    {
        private readonly IWebHost _webHost;

        public ServiceHost(IWebHost webHost)
        {
            _webHost = webHost;
        }
        public void Run() => _webHost.Run();

        public static HostBuilder Create<TStartUp>(string[] args) where TStartUp : class
        {
            Console.Title = typeof(TStartUp).Namespace;
            var config = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .AddCommandLine(args)
            .Build();

            var webHostBuilder = WebHost.CreateDefaultBuilder()
            .UseConfiguration(config).UseStartup<TStartUp>();

            return new HostBuilder(webHostBuilder.Build());
        }

        public abstract class BuilderBase
        {
            public abstract ServiceHost Build();
        }

        public class HostBuilder : BuilderBase
        {
            private readonly IWebHost _webHost;
            private IBusClient _bus;

            public HostBuilder(IWebHost webHost)
            {
                _webHost = webHost;
            }

            public BusBuilder UseRabbitMQ()
            {
                _bus = (IBusClient)_webHost.Services.GetService(typeof(IBusClient));

                return new BusBuilder(_webHost, _bus);
            }
            public override ServiceHost Build()
            {
                return new ServiceHost(_webHost);
            }
        }

        public class BusBuilder : BuilderBase
        {
            private readonly IWebHost _webHost;
            private IBusClient _bus;
            public BusBuilder(IWebHost webHost, IBusClient bus)
            {
                _webHost = webHost;
                _bus = bus;
            }

            public BusBuilder Subscribe2Command<TCommad>() where TCommad : ICommand
            {
                var handler = (ICommandHandler<TCommad>)_webHost.Services.GetService(typeof(ICommandHandler<TCommad>));
                _bus.WithCommandHandlerAsync(handler);   
                return this; 
            }

             public BusBuilder Subscribe2Event<TEvent>() where TEvent : IEvent
            {
                var handler = (IEventHanler<TEvent>)_webHost.Services.GetService(typeof(IEventHanler<TEvent>));
                _bus.WithEventHandlerAsync(handler);   
                return this; 
            }

            public override ServiceHost Build()
            {
                return new ServiceHost(_webHost);
            }
        }
    }
}