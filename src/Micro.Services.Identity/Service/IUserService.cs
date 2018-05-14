using System.Threading.Tasks;

namespace Micro.Services.Identity.Service
{
    public interface IUserService
    {
         Task RegistrAsync(string email, string pswrd, string name);
         Task LogAsync(string email, string pswrd);
    }
}