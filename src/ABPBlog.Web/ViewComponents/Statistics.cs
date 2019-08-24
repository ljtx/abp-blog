using ABPBlog.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABPBlog.Web.ViewComponents
{
    public class Statistics : ViewComponent
    {
        private readonly ABPBlogDbContext db;
        private IMemoryCache _memoryCache;
        private string cachekey = "statistics";

        public Statistics(ABPBlogDbContext context, IMemoryCache memoryCache)
        {
            db = context;
            _memoryCache = memoryCache;
        }

        public IViewComponentResult Invoke()
        {
            var allstatistics = new Tuple<int, int, int>(0, 0, 0);
            if (!_memoryCache.TryGetValue(cachekey, out allstatistics))
            {
                var usercount = 1;
                var topiccount = db.Topics.Count();
                var replycount = db.TopicReplys.Count();
                allstatistics = new Tuple<int, int, int>(usercount, topiccount, replycount);
                _memoryCache.Set(cachekey, allstatistics, TimeSpan.FromMinutes(1));
            }
            return View(allstatistics);
        }
    }
}
