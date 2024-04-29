using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public sealed class HomeController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public ActionResult<string> SecretEndpoint()
        {
            return this.Ok("Secret Message");
        }
    }
}