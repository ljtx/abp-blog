using Abp.AspNetCore.Mvc.Views;

namespace ABPBlog.Web.Views
{
    public abstract class ABPBlogRazorPage<TModel> : AbpRazorPage<TModel>
    {
        protected ABPBlogRazorPage()
        {
            LocalizationSourceName = ABPBlogConsts.LocalizationSourceName;
        }
    }
}
