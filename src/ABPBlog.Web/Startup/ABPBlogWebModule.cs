using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ABPBlog.Configuration;
using ABPBlog.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace ABPBlog.Web.Startup
{
    [DependsOn(
        typeof(ABPBlogApplicationModule), 
        typeof(ABPBlogEntityFrameworkCoreModule), 
        typeof(AbpAspNetCoreModule))]
    public class ABPBlogWebModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public ABPBlogWebModule(IHostingEnvironment env)
        {
            _appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName);
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(ABPBlogConsts.ConnectionStringName);

            Configuration.Navigation.Providers.Add<ABPBlogNavigationProvider>();

            Configuration.Modules.AbpAspNetCore()
                .CreateControllersForAppServices(
                    typeof(ABPBlogApplicationModule).GetAssembly()
                );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ABPBlogWebModule).GetAssembly());
        }
    }
}