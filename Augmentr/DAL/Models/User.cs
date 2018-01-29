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

<<<<<<< HEAD
        public string tokenize()
        {
            return GetHashCode().ToString() + Convert.ToInt32(Email).ToString();
        }

        public User(string Email, string Password )
        {
            this.Email = Email;
            this.Password = Password;
        }
=======
        public IList<Tag> Tags { get; set; }
>>>>>>> af827127217fe605171dae15f7a829a870232f80
    }
}