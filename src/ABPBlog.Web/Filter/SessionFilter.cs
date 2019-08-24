using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ABPBlog.Web.Filter
{
    public class SessionFilter: Attribute,IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("action执行之后");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var result = context.HttpContext.Session.GetString("abpblogsession");
            if (result == null)
            {
                context.Result = new RedirectResult("/Account/Login");
                return;
            }
        }
    }
}
