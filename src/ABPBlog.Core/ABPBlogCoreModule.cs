using Abp.Modules;
using Abp.Reflection.Extensions;
using ABPBlog.Localization;

namespace ABPBlog
{
    public class ABPBlogCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            ABPBlogLocalizationConfigurer.Configure(Configuration.Localization);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ABPBlogCoreModule).GetAssembly());
        }
    }
}