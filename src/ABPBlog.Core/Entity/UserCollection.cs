using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ABPBlog.Entity
{
    public class UserCollection : Entity<int>
    {
        public string UserId { get; set; }
        public int TopicId { get; set; }
        public Topic Topic { get; set; }
        public int State { get; set; }
        public DateTime CreateOn { get; set; }
    }
}
