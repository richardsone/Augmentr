using System;
using System.Collections.Generic;

namespace Augmentr.Domain.Models
{
    public class TagListResponse
    {
        public UserResponse User { get; set; }
        public IList<TagResponse> tags { get; set; }
    }
}