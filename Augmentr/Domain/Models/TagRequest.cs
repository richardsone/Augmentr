using System;

namespace Augmentr.Domain.Models
{
    public class TagRequest
    {
        public string Token { get; set; }
        public int Id { get; set; }
        public string Location { get; set; }
        public string Content { get; set; }
        public DateTime TimePosted { get; set; }
    }
}