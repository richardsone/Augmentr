using Microsoft.AspNetCore.Mvc;

namespace Augmentr.Controllers
{
    [Route("api/v1/[controller]")]
    public class UserController : Controller
    {
        // GET: api/v1/user/tags
        [HttpGet("tags")]
        public void LoadTags()
        {
        }

        // GET: api/v1/user/tags/:id
        [HttpGet("tags/{id}")]
        public void LoadTag(int id)
        {
        }

        // POST: api/v1/user/tags
        [HttpPost("tags")]
        public void CreateTag()
        {
        }

        // PUT: api/v1/user/tags
        [HttpPut("tags")]
        public void UpdateTag()
        {
        }

        // DELETE: api/v1/user/tags
        [HttpDelete("tags")]
        public void DeletTag()
        {
        }
    }
}
