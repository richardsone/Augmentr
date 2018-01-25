using Microsoft.AspNetCore.Mvc;

namespace Augmentr.Controllers
{
    [Route("api/v1/[controller]")]
    public class AdminController : Controller
    {
        // POST: api/v1/admin/user/{id}/destroy
        [HttpDelete("user/{id}/destroy")]
        public void DeleteAccount()
        {
        }

        // DELETE: api/v1/admin/tags
        [HttpPut("tags")]
        public void RemoveTag()
        {
        }

        // POST: api/v1/admin/add_roles
        [HttpDelete("add_roles")]
        public void AddRole()
        {
        }
    }
}
