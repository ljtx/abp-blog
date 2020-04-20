using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ABPBlog.Entity
{
    public class TopicReply : Entity<int>
    {
        public int TopicId { get; set; }
        public int ReplyUserId { get; set; }
        public User ReplyUser { get; set; }
        public string ReplyEmail { get; set; }
        public string ReplyContent { get; set; }
        public DateTime CreateOn { get; set; }
    }
}
