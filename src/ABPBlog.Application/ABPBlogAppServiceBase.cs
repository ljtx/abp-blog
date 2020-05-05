using Abp.Application.Services;

namespace ABPBlog
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class ABPBlogAppServiceBase : ApplicationService
    {
        protected ABPBlogAppServiceBase()
        {
            LocalizationSourceName = ABPBlogConsts.LocalizationSourceName;
        }
    }
}