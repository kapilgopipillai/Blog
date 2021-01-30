using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Entity
{
    public class PostEntity
    {
        public Guid Id { get; set; }
        public Guid authorId { get; set; }
        public string title { get; set; }
        public string metaTitle { get; set; }
        public string summary { get; set; }
        public bool published { get; set; }
        public string content { get; set; }
        public bool Disabled { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Modified { get; set; }
        public string ModifiedBy { get; set; }
    }
}
