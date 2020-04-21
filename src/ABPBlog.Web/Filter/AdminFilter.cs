using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABPBlog.Web.Filter
{
    public class AdminFilter: Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("action执行之后");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session;
            if (session != null)
            {
                var result = session.GetString("abpblogsession");
                if (result != null)
                {
                    var str = CompressHelper.AES_Decrypt(result, "qwertyuiop", "1234567891234567");
                    if (str != "ljtx")
                    {
                        context.Result = new RedirectResult("/Home/Index");
                        return;
                    }
                }
            }

        }
    }
}
