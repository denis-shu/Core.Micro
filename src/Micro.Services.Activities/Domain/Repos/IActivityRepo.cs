using System;
using System.Threading.Tasks;
using Micro.Services.Activities.Domain.Models;

namespace Micro.Services.Activities.Domain.Repos
{
    public interface IActivityRepo
    {
         Task<Activity> GetByIdAsync(Guid id);
         Task AddAsync(Activity activity);
    }
}