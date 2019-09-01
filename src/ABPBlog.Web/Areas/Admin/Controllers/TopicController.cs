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
    [Authorize("Admin")]
    public class TopicController: ABPBlogAdminBaseController
    {

        private IRepository<Topic> _topicRepository;
        private IRepository<TopicReply> _topicReplyRepository;
        public TopicController(IRepository<Topic> topicRepository, IRepository<TopicReply> topicReplyRepository)
        {
            _topicRepository = topicRepository;
            _topicReplyRepository = topicReplyRepository;
        }
        public IActionResult Index()
        {
            var pagesize = 20;
            var pageindex = 1;
            if (!string.IsNullOrEmpty(Request.Query["page"]))
                pageindex = Convert.ToInt32(Request.Query["page"]);
            var topics = _topicRepository.GetAll();
            var count = topics.Count();
            var topiclist = topics
                .OrderByDescending(r => r.CreateOn)
                .OrderByDescending(r => r.Top)
                .Skip(pagesize * (pageindex - 1))
                .Take(pagesize).ToList();
            ViewBag.PageIndex = pageindex;
            ViewBag.PageCount = count % pagesize == 0 ? count / pagesize : count / pagesize + 1;
            return View(topiclist);
        }

        public IActionResult Delete(int id)
        {
            var topic = _topicRepository.GetAll().FirstOrDefault(r => r.Id == id);
            if (topic != null)
            {
                 _topicRepository.Delete(topic);
                var replys = _topicReplyRepository.GetAllList(r => r.TopicId == id).Select(o=>o.Id).ToList();
                _topicReplyRepository.Delete(o=> replys.Contains(o.Id));
                return RedirectToAction("Index");
            }
            return Content("出现异常");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditSave(Topic topic)
        {
            _topicRepository.Update(topic);
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id, string type)
        {
            var topic = _topicRepository.GetAll().FirstOrDefault(r => r.Id == id);
            if (topic != null)
            {
                switch (type)
                {
                    case "top":
                        topic.Top = 1;
                        break;
                    //case "good":
                    //    topic.Good = true;
                    //    break;
                    case "notop":
                        topic.Top = 0;
                        break;
                        //case "nogood":
                        //    topic.Good = false;
                        //    break;
                }
                _topicRepository.Update(topic); 
                return RedirectToAction("Index");
            }
            return Content("出现异常");
        }
    }
}
