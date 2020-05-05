using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ABPBlog.Entity
{
    public class TopicNode : Entity<int>
    {
        public int ParentId { get; set; }
        public string NodeName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public DateTime CreateOn { get; set; }
    }
}
