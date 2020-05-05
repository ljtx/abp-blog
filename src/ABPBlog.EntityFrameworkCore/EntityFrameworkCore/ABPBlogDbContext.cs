using Abp.EntityFrameworkCore;
using ABPBlog.Entity;
using Microsoft.EntityFrameworkCore;

namespace ABPBlog.EntityFrameworkCore
{
    public class ABPBlogDbContext : AbpDbContext
    {
        //Add DbSet properties for your entities...
        public DbSet<Topic> Topics { get; set; }
        public DbSet<TopicReply> TopicReplys { get; set; }
        public DbSet<TopicNode> TopicNodes { get; set; }
        public DbSet<UserMessage> UserMessages { get; set; }
        public DbSet<UserCollection> UserCollection { get; set; }
        public DbSet<User> User { get; set; }
        public ABPBlogDbContext(DbContextOptions<ABPBlogDbContext> options) 
            : base(options)
        {

        }
      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Topic>().ToTable("topic");
            modelBuilder.Entity<TopicReply>().ToTable("topicreply");
            modelBuilder.Entity<TopicNode>().ToTable("topicnode");
            modelBuilder.Entity<User>().ToTable("user");
            modelBuilder.Entity<UserMessage>().ToTable("usermessage");
            modelBuilder.Entity<UserCollection>().ToTable("usercollection");
        }
    }
}
