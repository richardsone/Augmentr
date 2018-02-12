using System;
using System.Collections.Generic;
using Augmentr.Domain;
using Augmentr.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Augmentr.Controllers
{
    [Route("api/v1/[controller]")]
    public class UserController : Controller
    {
        private readonly ITagRepository _tagRepository;
        private readonly IUserRepository _userRepository;

        public UserController(ITagRepository tagRepository, IUserRepository userRepository)
        {
            _tagRepository = tagRepository;
            _userRepository = userRepository;
        }

        // GET: api/v1/user/tags
        [HttpGet("tags/user/{userEmail}")]
        public IActionResult LoadTags(string userEmail)
        {
            var registerRequest = new RegisterRequest();
            registerRequest.Email = userEmail;
            registerRequest.Name = "Ian";
            registerRequest.Password = "password";
            _userRepository.TryRegister(registerRequest);
            var tags = _tagRepository.LoadTagForUser(userEmail);
            return Ok(tags);
        }

        // GET: api/v1/user/tags/:id
        [HttpGet("tags/{id}")]
        public IActionResult LoadTag(int id)
        {
            var tag = _tagRepository.LoadTag(id);

            if (tag == null)
                return NotFound(id);

            return Ok(tag);
        }

        // POST: api/v1/user/tags
        [HttpPost("tags")]
        public IActionResult CreateTag([FromBody] TagRequest request)
        {
            try {
                _tagRepository.CreateTag(request);

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // PUT: api/v1/user/tags
        [HttpPut("tags")]
        public IActionResult UpdateTag([FromBody] TagRequest request)
        {
            try {
                _tagRepository.UpdateTag(request);

                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(request.Id);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // DELETE: api/v1/user/tags
        [HttpDelete("tags")]
        public IActionResult DeleteTag([FromBody] TagRequest request)
        {
            try {
                _tagRepository.DeleteTag(request);

                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(request.Id);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // GET: api/v1/user/query
        [HttpPost("query")]
        public IActionResult Query([FromBody] string queryString)
        {
            var user = _userRepository.Query(queryString);
            return Ok(user);
        }
    }
}
