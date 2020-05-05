using Abp.AspNetCore.Mvc.Controllers;

namespace ABPBlog.Web.Controllers
{
    public abstract class ABPBlogControllerBase: AbpController
    {
        protected ABPBlogControllerBase()
        {
            LocalizationSourceName = ABPBlogConsts.LocalizationSourceName;
        }
    }
}