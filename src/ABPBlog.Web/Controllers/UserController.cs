using Abp.Domain.Repositories;
using ABPBlog.Entity;
using ABPBlog.IRepository;
using ABPBlog.Web.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ABPBlog.Web.Controllers
{
    [Authorize]
    public class UserController: ABPBlogControllerBase
    {
        private ITopicRepository _topicRepository;
        private IRepository<TopicReply> _replyRepository;
        private IRepository<User> _userRepository;
        private IHostingEnvironment _env;
        public UserController(ITopicRepository topicRepository, IRepository<TopicReply> replyRepository, IRepository<User> userRepository, IHostingEnvironment env)
        {
            _topicRepository = topicRepository;
            _replyRepository = replyRepository;
            _userRepository = userRepository;
            _env = env;
        }
        //public IActionResult Index()
        //{
            //var u = userRepository.GetUserAsync(User).Result;
            //var topics = _topic.List(r => r.UserId == u.Id).ToList();
            //var replys = _reply.List(r => r.ReplyUserId == u.Id).ToList();
            //ViewBag.Topics = topics;
            //ViewBag.Replys = replys;
            //return View(u);
       // }

        //public IActionResult Edit()
        //{
        //    //return View(UserManager.GetUserAsync(User).Result);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(UserViewModel usermodel)
        //{
            //var user = UserManager.GetUserAsync(User).Result;
            //if (ModelState.IsValid)
            //{
            //    if (usermodel.Avatar != null)
            //    {
            //        var avatar = usermodel.Avatar;
            //        if (avatar.Length / 1024 > 100)
            //        {
            //            return Content("头像文件大小超过100KB");
            //        }
            //        var ext = Path.GetExtension(avatar.FileName);
            //        var avatarfile = user.Id + ext;
            //        var avatarpath = Path.Combine(_env.WebRootPath, "images", "avatar");
            //        if (!Directory.Exists(avatarpath))
            //            Directory.CreateDirectory(avatarpath);
            //        var filepath = Path.Combine(avatarpath, avatarfile);
            //        using (FileStream fs = new FileStream(filepath, FileMode.Create))
            //        {
            //            avatar.CopyTo(fs);
            //            fs.Flush();
            //        }
            //        user.Avatar = $"/images/avatar/{avatarfile}";
            //    }
            //    user.Email = usermodel.Email;
            //    user.Url = usermodel.Url;
            //    user.GitHub = usermodel.GitHub;
            //    user.Profile = usermodel.Profile;
            //    await UserManager.UpdateAsync(user);
            //    return RedirectToAction("Index");
            //}
            //return View(user);
       // }
    }
}
