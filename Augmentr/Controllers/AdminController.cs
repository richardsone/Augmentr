using System;
using System.Collections.Generic;
using Augmentr.Domain;
using Augmentr.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Augmentr.Controllers
{
    [Route("api/v1/[controller]")]
    public class AdminController : Controller
    {
        private readonly IAdminRepository _adminRepository;

        public AdminController(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        // GET: /api/v1/admin/tags/{token}
        [HttpGet("tags/{token}")]
        public IActionResult LoadAllTags(string token)
        {
            var tags = _adminRepository.LoadAllTags(token);

            if (tags == null)
            {
                return Unauthorized();
            }
            return Ok(tags);
        }

        // PUT: api/v1/admin/tags
        [HttpPut("tags")]
        public IActionResult UpdateTag([FromBody] TagRequest request)
        {
            try {
                _adminRepository.UpdateTag(request);

                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // POST: api/v1/admin/tags/destroy
        [HttpPost("tags/destroy")]
        public IActionResult DeleteTag([FromBody] TagRequest request)
        {
            try {
                _adminRepository.DeleteTag(request);

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

        // POST: api/v1/admin/add_roles
        [HttpDelete("add_roles")]
        public void AddRole()
        {
        }
    }
}
