using System.Threading.Tasks;
using Micro.Base.Exceptions;
using Micro.Services.Identity.Domain.Models;
using Micro.Services.Identity.Domain.Repos;

namespace Micro.Services.Identity.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly IPswrdEncr _encr;

        public UserService(IUserRepo userRepo, IPswrdEncr encr)
        {
            _userRepo = userRepo;
            _encr = encr;
        }
        public async Task LogAsync(string email, string pswrd)
        {
            var user = await _userRepo.GetASync(email);
            if (user == null)
                throw new MicroException("user_not_exist", $"user_not_exist");

            if (!user.IsPswrdCorrect(pswrd, _encr))
                throw new MicroException("something is wrong with the password", 
                $"something is wrong with the password");

        }

        public async Task RegistrAsync(string email, string pswrd, string name)
        {
            var user = await _userRepo.GetASync(email);
            if (user != null)
                throw new MicroException("user_exist", $"Email: '{email}' is used");

            user = new User(email, name);
            user.AddPassword(pswrd, _encr);

            await _userRepo.AddAsync(user);

        }
    }
}