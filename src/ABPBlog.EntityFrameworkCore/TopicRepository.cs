using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;
using ABPBlog.Entity;
using ABPBlog.EntityFrameworkCore;
using ABPBlog.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ABPBlog
{
    public class TopicRepository: ABPBlogRepositoryBase<Topic>,ITopicRepository
    {

        public TopicRepository(IDbContextProvider<ABPBlogDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public Page<Topic> PageList(int pagesize = 20, int pageindex = 1)
        {
            return PageList(null, pagesize, pageindex);
        }

        public Page<Topic> PageList(Expression<Func<Topic, bool>> predicate, int pagesize = 20, int pageindex = 1)
        {
            var topics = GetAll().Include(r => r.User).Include(r => r.Node).Include(r => r.LastReplyUser).AsQueryable().AsNoTracking();
            if (predicate != null)
            {
                topics = topics.Where(predicate);
            }
            var count = topics.Count();
            topics = topics.OrderByDescending(r => r.CreateOn)
                    .OrderByDescending(r => r.Top)
                    .Skip((pageindex - 1) * pagesize).Take(pagesize);
         
            return new Page<Topic>(topics.ToList(), pagesize, count);
        }
    }
}
