using System;
using System.Threading.Tasks;
using Micro.Base.Commands;
using Micro.Base.Events;
using Micro.Base.Exceptions;
using Micro.Services.Activities.Services;
using Microsoft.Extensions.Logging;
using RawRabbit;

namespace Micro.Services.Activities.Handler
{
    public class CreateActivityHandler : ICommandHandler<CreateActivity>
    {
        private readonly IBusClient _bus;
        private readonly IActivityService activityServise;
        private ILogger _logger;

        public CreateActivityHandler(IBusClient bus, IActivityService activityServise,
                                    ILogger<CreateActivityHandler> logger)
        {
            _bus = bus;
            this.activityServise = activityServise;
            _logger = logger;
        }
        public async Task HandleAsync(CreateActivity command)
        {
            _logger.LogInformation($"Cr acti {command.Name}");

            try
            {
                await activityServise.AddAsync(command.Id, command.UserId,
                command.Category, command.Name, command.Description, command.CreatedAt);

                await _bus.PublishAsync(new ActivityCreated(command.Id, command.UserId,
                               command.Category, command.Name, command.Description, command.CreatedAt));

                return;
            }
            catch (MicroException e)
            {
                await _bus.PublishAsync(new CreateActivityRejected(command.Id, e.Code, e.Message));
                _logger.LogError(e.Message);
            }
            catch (Exception e)
            {
                await _bus.PublishAsync(new CreateActivityRejected(command.Id, "erroe", e.Message));
                _logger.LogError(e.Message);

            }


        }
    }
}