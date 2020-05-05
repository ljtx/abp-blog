using System;
using System.Threading.Tasks;
using Abp.TestBase;
using ABPBlog.EntityFrameworkCore;
using ABPBlog.Tests.TestDatas;

namespace ABPBlog.Tests
{
    public class ABPBlogTestBase : AbpIntegratedTestBase<ABPBlogTestModule>
    {
        public ABPBlogTestBase()
        {
            UsingDbContext(context => new TestDataBuilder(context).Build());
        }

        protected virtual void UsingDbContext(Action<ABPBlogDbContext> action)
        {
            using (var context = LocalIocManager.Resolve<ABPBlogDbContext>())
            {
                action(context);
                context.SaveChanges();
            }
        }

        protected virtual T UsingDbContext<T>(Func<ABPBlogDbContext, T> func)
        {
            T result;

            using (var context = LocalIocManager.Resolve<ABPBlogDbContext>())
            {
                result = func(context);
                context.SaveChanges();
            }

            return result;
        }

        protected virtual async Task UsingDbContextAsync(Func<ABPBlogDbContext, Task> action)
        {
            using (var context = LocalIocManager.Resolve<ABPBlogDbContext>())
            {
                await action(context);
                await context.SaveChangesAsync(true);
            }
        }

        protected virtual async Task<T> UsingDbContextAsync<T>(Func<ABPBlogDbContext, Task<T>> func)
        {
            T result;

            using (var context = LocalIocManager.Resolve<ABPBlogDbContext>())
            {
                result = await func(context);
                context.SaveChanges();
            }

            return result;
        }
    }
}
