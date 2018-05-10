using System;
using System.Threading.Tasks;
using Micro.Base.Commands;
using Micro.Base.Events;
using RawRabbit;

namespace Micro.Services.Activities.Handler
{
    public class CreateActivityHandler : ICommandHandler<CreateActivity>
    {
        private readonly IBusClient _bus;
        public CreateActivityHandler(IBusClient bus)
        {
            _bus = bus;
        }
        public async Task HandleAsync(CreateActivity command)
        {
            Console.WriteLine($"Cr acti {command.Name}");
            await _bus.PublishAsync(new ActivityCreated(
                command.Id,
                command.UserId,
                command.Category,
                command.Name
            ));
        }
    }
}