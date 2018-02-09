using Augmentr.Domain;
using Augmentr.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Augmentr.Controllers
{
    [Route("api/v1/[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IRequestVerificationPolicy _requestPolicy;

        public AuthController(IUserRepository userRepository, IRequestVerificationPolicy requestPolicy)
        {
            _userRepository = userRepository;
            _requestPolicy = requestPolicy;
        }

        // POST: api/v1/auth/login
        [HttpPost("[action]")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();

            if (_requestPolicy.VerifyRequest(ip)) 
            {
                var token = _userRepository.TryLogin(request);

                if (token == null) {
                    _requestPolicy.RecordBadRequest(ip);
                    return BadRequest("Username or password not correct");
                }

                _requestPolicy.RecordValidRequest(ip);
                return Ok(token);
            }
            return Unauthorized();
        }

        // POST: api/v1/auth/register
        [HttpPost("[action]")]
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
