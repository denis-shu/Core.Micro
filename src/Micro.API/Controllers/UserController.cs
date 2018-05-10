using System;
using System.Threading.Tasks;
using Micro.Base.Commands;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;

namespace Micro.API.Controllers
{
    public class UserController:Controller
    {
         private readonly IBusClient _busClient;

        public UserController(IBusClient busClient)
        {
            _busClient = busClient;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Post([FromBody]CreateUser command)
        {
            command.Email = "test@email.test";
            command.Name= "command_username";
            await _busClient.PublishAsync(command);

            return Accepted($"user/{command.Name}");
        }
    }
}