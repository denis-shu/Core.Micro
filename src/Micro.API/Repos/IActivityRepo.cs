using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Micro.API.Models;

namespace Micro.API.Repos
{
    public interface IActivityRepo
    {
        Task<Activity> GetAsync(Guid id);
        Task<IEnumerable<Activity>> BrowserAsync(Guid userId);
        Task AddASync(Activity model);
    }
}