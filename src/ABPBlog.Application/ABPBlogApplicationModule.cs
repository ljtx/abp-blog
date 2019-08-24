using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace ABPBlog
{
    [DependsOn(
        typeof(ABPBlogCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class ABPBlogApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ABPBlogApplicationModule).GetAssembly());
        }
    }
}