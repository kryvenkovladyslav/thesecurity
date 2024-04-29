using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Authentication.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public sealed class SimpleAuthenticationController : ControllerBase
    {
        private readonly ISimpleSignInOutService signInOutService;

        public SimpleAuthenticationController(ISimpleSignInOutService signInOutService)
        {
            this.signInOutService = signInOutService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> SimpleSignIn()
        {
            await this.signInOutService.SignInAsync(this.HttpContext);

            return await Task.FromResult(this.Ok());
        }

        [HttpGet]
        public async Task<ActionResult> SimpleSignOut()
        {
            await this.signInOutService.SignOutAsync(this.HttpContext);

            return await Task.FromResult(this.Ok());
        }
    }
}