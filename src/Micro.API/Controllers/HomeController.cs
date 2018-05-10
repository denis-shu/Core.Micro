using Microsoft.AspNetCore.Mvc;

namespace Micro.API.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {

        [HttpGet("")]
        public IActionResult Get()
        {
            return Content("Hello from MIcro.API");
        }
    }
}