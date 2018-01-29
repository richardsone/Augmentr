using Augmentr.Domain;
using Augmentr.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Augmentr.Controllers
{
    [Route("api/v1/[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // POST: api/v1/auth/register
        [HttpPost("[action]")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var token = _userRepository.TryLogin(request);

            if (token == null) {
                return BadRequest("Username or password not correct");
            }

            return Ok(token);
        }
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            var token = _userRepository.TryRegister(request);

            if (token == null) {
                return BadRequest("User already Exists");
            } 
            return Ok(token);
        }

        // POST: api/v1/auth/logout
        [HttpPost("[action]")]
        public void Logout()
        {
        }
    }
}
