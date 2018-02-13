using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace Augmentr.Dal.Models
{
    public class User
    {
        [Key]
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public Roles Role { get; set; }

        public IList<Tag> Tags { get; set; }

        public User() {
            Tags = new List<Tag>();
        }
    }
}
