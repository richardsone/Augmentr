using Microsoft.AspNetCore.Mvc;

namespace Augmentr.Controllers
{
    [Route("api/v1/[controller]")]
    public class AuthController : Controller
    {
        // POST: api/v1/auth/register
        [HttpPost("[action]")]
        public void Login()
        {
        }

        // POST: api/v1/auth/register
        [HttpPost("[action]")]
        public void Register()
        {
        }

        // POST: api/v1/auth/logout
        [HttpPost("[action]")]
        public void Logout()
        {
        }
    }
}
