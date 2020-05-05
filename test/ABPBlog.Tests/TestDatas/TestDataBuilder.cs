using ABPBlog.EntityFrameworkCore;

namespace ABPBlog.Tests.TestDatas
{
    public class TestDataBuilder
    {
        private readonly ABPBlogDbContext _context;

        public TestDataBuilder(ABPBlogDbContext context)
        {
            _context = context;
        }

        public void Build()
        {
            //create test data here...
        }
    }
}