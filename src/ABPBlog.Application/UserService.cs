using Abp.Domain.Repositories;
using ABPBlog.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using ABPBlog.IService;

namespace ABPBlog
{
    public class UserService: IUserService
    {
        private IRepository<User> _userRepository;
        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public User GetUserByName(string userName)
        {
            return _userRepository.GetAll().FirstOrDefault(o => o.UserName == userName);
        }
        public User SignIn(string userName,string passWord)
        {
            var users =_userRepository.GetAll();
            return users.FirstOrDefault(o => o.UserName == userName && o.PassWord == passWord);
        }

        public async Task<bool> CreateAsync(User user)
        {
            var item= SignIn(user.UserName, user.PassWord);
            if (item != null)
            {
                return false;
            }
            else
            {
                 await _userRepository.InsertAsync(user);
                return true;
            }
        }
    }
}
