using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ABPBlog.Web.Startup;
namespace ABPBlog.Web.Tests
{
    [DependsOn(
        typeof(ABPBlogWebModule),
        typeof(AbpAspNetCoreTestBaseModule)
        )]
    public class ABPBlogWebTestModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ABPBlogWebTestModule).GetAssembly());
        }
    }
}