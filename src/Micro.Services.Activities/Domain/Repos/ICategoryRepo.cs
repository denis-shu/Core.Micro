using System.Collections.Generic;
using System.Threading.Tasks;
using Micro.Services.Activities.Domain.Models;

namespace Micro.Services.Activities.Domain.Repos
{
    public interface ICategoryRepo
    {
         Task<Category> GetAsync(string name);
         Task<IEnumerable<Category>> BrowseAsync();

         Task AddAsync(Category categry);
    }
}