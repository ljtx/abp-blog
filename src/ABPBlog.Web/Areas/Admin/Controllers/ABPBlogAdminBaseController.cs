using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABPBlog.Web.Controllers;
using ABPBlog.Web.Filter;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ABPBlog.Web.Areas.Admin.Controllers
{
    [SessionFilter]
    [AdminFilter]
    public class ABPBlogAdminBaseController : ABPBlogControllerBase
    {
        
    }
}
