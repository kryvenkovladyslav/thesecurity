using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Authentication.WebApi
{
    public interface ISimpleSignInOutService
    {
        public Task SignInAsync(HttpContext context);

        public Task SignOutAsync(HttpContext context);
    }
}