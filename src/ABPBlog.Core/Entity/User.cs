using Abp.Domain.Entities;
using System;
namespace ABPBlog.Entity
{
    public class User:Entity<int>
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string Profile { get; set; }
        public string Url { get; set; }
        public string GitHub { get; set; }
        public int TopicCount { get; set; }
        public int TopicReplyCount { get; set; }
        public int Score { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime LastTime { get; set; }
    }
}
