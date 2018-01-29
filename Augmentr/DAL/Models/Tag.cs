using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Augmentr.Dal.Models
{
    public class Tag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public string Location { get; set; }
        public string Content { get; set; }
        public DateTime TimePosted { get; set; }

        public User User { get; set; }
    }
}