using System;

namespace Augmentr.Domain.Models
{
    public class TagResponse
    {
        public UserResponse User { get; set; }
        public string Location { get; set; }
        public string Content { get; set; }
        public DateTime TimePosted { get; set; }
    }
}