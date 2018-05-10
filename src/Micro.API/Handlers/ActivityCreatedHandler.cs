using System;
using System.Threading.Tasks;
using Micro.Base.Events;

namespace Micro.API.Handlers
{
    public class ActivityCreatedHandler : IEventHanler<ActivityCreated>
    {
        public  async Task HandleAsync(ActivityCreated @event)
        {
            await Task.CompletedTask;
            Console.WriteLine($"Test. Act Created  {@event.Name}");
        }
    }
}