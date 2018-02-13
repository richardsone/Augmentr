using System;

namespace Augmentr.Domain.Models
{
    public class TagResponse
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public string Location { get; set; }
        public string Content { get; set; }
        public DateTime TimePosted { get; set; }
    }
}