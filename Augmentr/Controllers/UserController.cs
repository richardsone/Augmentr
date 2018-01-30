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

        public UserController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        // GET: api/v1/user/tags
        [HttpGet("tags")]
        public void LoadTags()
        {
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
    }
}
