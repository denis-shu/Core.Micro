using System;
using System.Threading.Tasks;
using Micro.API.Models;
using Micro.API.Repos;
using Micro.Base.Events;

namespace Micro.API.Handlers
{
    public class ActivityCreatedHandler : IEventHanler<ActivityCreated>
    {
        private readonly IActivityRepo _repo;
        public ActivityCreatedHandler(IActivityRepo repo)
        {
            this._repo = repo;

        }

        public async Task HandleAsync(ActivityCreated ev)
        {
            await _repo.AddASync(new Activity
            {
                Id = ev.Id,
                NAme = ev.Name,
                UserId = ev.UserId,
                Desciptions = ev.Description,
                Category = ev.Category,
                CreatedAt = ev.CreatedAt

            });
            Console.WriteLine($"Act Created  {ev.Name}");
        }
    }
}