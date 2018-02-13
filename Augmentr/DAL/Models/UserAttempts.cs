using System;
using System.ComponentModel.DataAnnotations;

namespace Augmentr.Dal.Models
{
    public class UserAttempt
    {
        [Key]
        public string IP { get; set; }
        public int Attempts { get; set; }
        public DateTime Timeout { get; set; }
        public DateTime LastRequest { get; set; }
    }
}