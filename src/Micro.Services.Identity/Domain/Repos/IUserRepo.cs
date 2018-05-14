using System;
using System.Threading.Tasks;
using Micro.Services.Identity.Domain.Models;

namespace Micro.Services.Identity.Domain.Repos
{
    public interface IUserRepo
    {
        Task<User> GetASync(Guid id);
        Task<User> GetASync(string email);

        Task AddAsync(User u);
    }
}