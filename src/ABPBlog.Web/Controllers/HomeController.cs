using Abp.Domain.Repositories;
using ABPBlog.Entity;
using ABPBlog.IRepository;
using ABPBlog.IService;
using ABPBlog.Web.ViewModel;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;

namespace ABPBlog.Web.Controllers
{
    public class HomeController : ABPBlogControllerBase
    {
        private ITopicRepository _topicRepository;
        private IRepository<TopicNode> _topicNodeRepository;
        private IRepository<User> _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IUserService _userService;
        public HomeController(ITopicRepository topicRepository, IRepository<TopicNode> topicNodeRepository, IRepository<User> userRepository, IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            _topicRepository = topicRepository;
            _topicNodeRepository = topicNodeRepository;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }
        public ActionResult Index()
        {
            var pagesize = 20;
            var pageindex = 1;
            Page<Topic> result = null;
            if (!string.IsNullOrEmpty(Request.Query["page"]))
                pageindex = Convert.ToInt32(Request.Query["page"]);
            if (!string.IsNullOrEmpty(Request.Query["s"]))
                result = _topicRepository.PageList(r => r.Title.Contains(Request.Query["s"]), pagesize, pageindex);
            else
                result = _topicRepository.PageList(pagesize, pageindex);
            ViewBag.Topics = result.List.Select(r => new TopicViewModel
            {
                Id = r.Id,
                NodeId = r.Node.Id,
                NodeName = r.Node.Name,
                UserName = r.User.UserName,
               // Avatar = r.User.Avatar,
                Title = r.Title,
                Top = r.Top,
                Type = r.Type,
                ReplyCount = r.ReplyCount,
                LastReplyTime = r.LastReplyTime,
                CreateOn = r.CreateOn
            }).ToList();
            ViewBag.PageIndex = pageindex;
            ViewBag.PageCount = result.GetPageCount();
            ViewBag.User = GetCurrentUser();
            var nodes = _topicNodeRepository.GetAll().ToList();
            ViewBag.Nodes = nodes;
            ViewBag.NodeListItem = nodes.Where(r => r.ParentId != 0).Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.Name });
            return View();
        }
        private User GetCurrentUser()
        {
            string str = _httpContextAccessor.HttpContext.Session.GetString("abpblogsession");
            if (!string.IsNullOrWhiteSpace(str))
            {
                str = CompressHelper.AES_Decrypt(str, "qwertyuiop", "1234567891234567");
                return _userService.GetUserByName(str);
            }
            return null;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Topic topic)
        {
            if (ModelState.IsValid)
            {
                topic.CreateOn = DateTime.Now;
                topic.Type = TopicType.Normal;
                topic.LastReplyUserId = topic.UserId;
                _topicRepository.Insert(topic);
            }
            return RedirectToAction("Index");
        }
        public IActionResult About()
        {
            ViewData["Message"] = "ABPBlog";
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}