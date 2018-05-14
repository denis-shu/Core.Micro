using System;
using System.Threading.Tasks;
using Micro.Base.Commands;
using Micro.Base.Events;
using Micro.Base.Exceptions;
using Micro.Services.Identity.Service;
using Microsoft.Extensions.Logging;
using RawRabbit;

namespace Micro.Services.Identity.Handlers
{
    public class CreateUserHandler : ICommandHandler<CreateUser>
    {
        private readonly IBusClient _bus;
        private readonly IUserService _userService;
        private ILogger _logger;

        public CreateUserHandler(IBusClient bus, IUserService userService)
        {
            _bus = bus;
            _userService = userService;
        }
        public async Task HandleAsync(CreateUser command)
        {
            _logger.LogInformation($"Create user {command.Name}");

            try
            {
                await _userService.RegistrAsync(command.Email, command.Password, command.Name);

                await _bus.PublishAsync(new UserCreated(command.Email, command.Name));

                return;
            }
            catch (MicroException e)
            {
                await _bus.PublishAsync(new CreateUserRejected(command.Email, e.Code, e.Message));
                _logger.LogError(e.Message);
            }
            catch (Exception e)
            {
                await _bus.PublishAsync(new CreateUserRejected(command.Email, "erroe", e.Message));
                _logger.LogError(e.Message);

            }
          
        }
    }
}