using System;
using System.Threading.Tasks;
using Micro.Base.Events;

namespace Micro.API.Handlers
{
    public class UserCreatedHandler : IEventHanler<UserCreated>
    {
        public UserCreatedHandler()
        {
            
        }
        public async Task HandleAsync(UserCreated ev)
        {
           await Task.CompletedTask;
            Console.WriteLine($"Test. user Created  {ev.Email}");
        }
    }
}