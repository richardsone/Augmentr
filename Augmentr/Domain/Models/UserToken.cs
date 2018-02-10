using System;
using Augmentr.Dal.Models;

namespace Augmentr.Domain.Models
{
    public class UserToken
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime exp { get; set; }
        public Roles Role { get; set; }
    }
}