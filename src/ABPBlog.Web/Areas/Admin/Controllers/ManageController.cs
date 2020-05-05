using Abp.Domain.Repositories;
using ABPBlog.Entity;
using ABPBlog.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABPBlog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManageController : ABPBlogAdminBaseController
    {
        private IRepository<User> _userRepository;
        private IRepository<Topic> _topicRepository;
        private IRepository<TopicReply> _topicReplyRepository;
        public ManageController(IRepository<User> userRepository, IRepository<Topic> topicRepository, IRepository<TopicReply> topicReplyRepository)
        {
            _userRepository = userRepository;
            _topicRepository = topicRepository;
            _topicReplyRepository = topicReplyRepository;
        }
        public IActionResult Index()
        {
            var usercount = _userRepository.Count();
            var topiccount = _topicRepository.Count();
            var replycount = _topicReplyRepository.Count();
            var allstatistics = new Tuple<int, int, int>(usercount, topiccount, replycount);
            ViewBag.Statistics = allstatistics;
            var topics = _topicRepository.GetAll().OrderByDescending(r => r.CreateOn).Take(10).ToList();
            return View(topics);
        }
    }
    }
