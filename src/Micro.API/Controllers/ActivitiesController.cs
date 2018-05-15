using System;
using System.Linq;
using System.Threading.Tasks;
using Micro.API.Repos;
using Micro.Base.Commands;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;

namespace Micro.API.Controllers
{
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class ActivitiesController : Controller
    {
        private readonly IBusClient _busClient;
        private readonly IActivityRepo _repo;

        public ActivitiesController(IBusClient busClient, IActivityRepo repo)
        {
            this._repo = repo;
            _busClient = busClient;

        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]CreateActivity command)
        {
            //Console.Write("start");
            command.Id = Guid.NewGuid();
            command.CreatedAt = DateTime.UtcNow;
            command.UserId = Guid.Parse(User.Identity.Name);

            await _busClient.PublishAsync(command);

            return Accepted($"activities/{command.Id}");
        }


        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var activitiess = await _repo
            .BrowserAsync(Guid.Parse(User.Identity.Name));

            var res = activitiess.Select(s => new
            {
                s.Id,
                s.NAme,
                s.Category,
                s.CreatedAt
            });
            return Json(res);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var activity = await _repo
            .GetAsync(id);

            if (activity == null)
                return NotFound();

            if (activity.UserId != Guid.Parse(User.Identity.Name))
                return Unauthorized();
           
            return Json(activity);

        }
    }
}