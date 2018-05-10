using System.Reflection;
using System.Threading.Tasks;
using Micro.Base.Commands;
using Micro.Base.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using RawRabbit.Instantiation;
using RawRabbit.Pipe;

namespace Micro.Base.RabbitMQ
{
    public static class RabbitMQExt
    {
        public static void AddRabbitMQ(this IServiceCollection servi, IConfiguration config)
        {
            var opt = new RabbitMQOpt();
            var section = config.GetSection("rabbitmq");
            section.Bind(opt);

            var client = RawRabbitFactory.CreateSingleton(new RawRabbitOptions
            {
                ClientConfiguration = opt
            });

            servi.AddSingleton<IBusClient>(x => client);
        }
        public static Task WithCommandHandlerAsync<TCommmand>(this IBusClient bus,
        ICommandHandler<TCommmand> handler) where TCommmand : ICommand
        {
            //var task =
            return bus.SubscribeAsync<TCommmand>(msg =>
            handler.HandleAsync(msg),
            context =>
                context.UseConsumerConfiguration(configuration =>
                    configuration.FromDeclaredQueue(queue =>
                        queue.WithName(GetQueueNAme<TCommmand>()))));

        }


        public static Task WithEventHandlerAsync<TEvent>(this IBusClient bus,
       IEventHanler<TEvent> handler) where TEvent : IEvent
        {
            return bus.SubscribeAsync<TEvent>(msg =>
            handler.HandleAsync(msg),
            context =>
                context.UseConsumerConfiguration(configuration =>
                    configuration.FromDeclaredQueue(queue =>
                        queue.WithName(GetQueueNAme<IEvent>()))));

        }

        private static string GetQueueNAme<T>()
        {
            return $"{Assembly.GetEntryAssembly().GetName()}/{typeof(T).Name}";
        }


    }
}