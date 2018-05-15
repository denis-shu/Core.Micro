using System;
using System.Threading.Tasks;
using Micro.Base.Events;

namespace Micro.API.Handlers
{
    public class UserAuthenticatedHandler : IEventHanler<UserAuthenticated>
    {
        public async Task HandleAsync(UserAuthenticated ev)
        {
            await Task.CompletedTask;
            Console.WriteLine($"Test. User authenticted  {ev.Email}");
        }
    }
}