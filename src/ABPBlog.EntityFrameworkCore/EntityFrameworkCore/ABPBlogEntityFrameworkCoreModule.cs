using Abp.EntityFrameworkCore;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace ABPBlog.EntityFrameworkCore
{
    [DependsOn(
        typeof(ABPBlogCoreModule), 
        typeof(AbpEntityFrameworkCoreModule))]
    public class ABPBlogEntityFrameworkCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ABPBlogEntityFrameworkCoreModule).GetAssembly());
        }
    }
}