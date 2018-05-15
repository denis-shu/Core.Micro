using System.Threading.Tasks;
using Micro.Base.Commands;
using Micro.Services.Identity.Service;
using Microsoft.AspNetCore.Mvc;

namespace Micro.Services.Identity.Controllers
{
    [Route("")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;

        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticateUser cmd)
        {
            return Json(await _userService.LogAsync(cmd.Email, cmd.Password));
        }
    }
}