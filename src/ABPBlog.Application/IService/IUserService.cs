using Abp.Application.Services;
using ABPBlog.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ABPBlog.IService
{
    public interface IUserService: IApplicationService
    {
         User SignIn(string userName, string passWord);
         Task<bool> CreateAsync(User user);
    }
}
