using System.Threading.Tasks;
using Micro.Base.Auth;

namespace Micro.Services.Identity.Service
{
    public interface IUserService
    {
         Task RegistrAsync(string email, string pswrd, string name);
         Task<JsonWebToken> LogAsync(string email, string pswrd);
    }
}