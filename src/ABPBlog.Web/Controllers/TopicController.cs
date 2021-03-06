﻿using Abp.Domain.Repositories;
using ABPBlog.Entity;
using ABPBlog.IRepository;
using ABPBlog.Web.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ABPBlog.Web.Controllers
{
    public class TopicController: ABPBlogControllerBase
    {
        private ITopicRepository _topicRepository;
        private IRepository<TopicNode> _nodeRepository;
        private IRepository<TopicReply> _replyRepository;
        private IRepository<User> _userRepository;

        public TopicController(ITopicRepository topic, IRepository<TopicNode> node, IRepository<TopicReply> reply, IRepository<User> userRepository)
        {
            _topicRepository = topic;
            _nodeRepository = node;
            _replyRepository = reply;
            _userRepository = userRepository;
        }
        [Route("/Topic/{id}")]
        public IActionResult Index(int id)
        {
            if (id <= 0) return Redirect("/");
            var topic = _topicRepository.Get(id);
            if (topic == null) return Redirect("/");
            topic.Node = _nodeRepository.Get(topic.NodeId);
            topic.User = _userRepository.Get(topic.UserId);
            var replys = _replyRepository.GetAllList(r => r.TopicId == id);
            topic.ViewCount += 1;
            _topicRepository.Update(topic);
            ViewBag.Replys = replys;
            return View(topic);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Topic/{id}")]
        public IActionResult Index([Bind("TopicId,ReplyUserId,ReplyEmail,ReplyContent")]TopicReply reply)
        {
            if (ModelState.IsValid && !string.IsNullOrEmpty(reply.ReplyContent))
            {
                reply.CreateOn = DateTime.Now;
                _replyRepository.Insert(reply);
                var topic = _topicRepository.Get(reply.TopicId);
                topic.LastReplyUserId = reply.ReplyUserId;
                topic.LastReplyTime = reply.CreateOn;
                topic.ReplyCount += 1;
                _topicRepository.Update(topic);
            }
            return RedirectToAction("Index", "Topic", new { Id = reply.TopicId });
        }
        [Route("/Topic/Node/{name}")]
        public IActionResult Node(string name)
        {
            if (string.IsNullOrEmpty(name)) return Redirect("/");
            var node = _nodeRepository.GetAllList(r => r.NodeName == name).FirstOrDefault();
            if (node == null)
                node = _nodeRepository.Get(Convert.ToInt32(name));
            if (node == null) return Redirect("/");
            var pagesize = 20;
            var pageindex = 1;
            Page<Topic> result;
            if (!string.IsNullOrEmpty(Request.Query["page"]))
                pageindex = Convert.ToInt32(Request.Query["page"]);
            if (!string.IsNullOrEmpty(Request.Query["s"]))
                result = _topicRepository.PageList(r => r.NodeId == node.Id && r.Title.Contains(Request.Query["s"]), pagesize, pageindex);
            else
                result = _topicRepository.PageList(r => r.NodeId == node.Id, pagesize, pageindex);
            ViewBag.Topics = result.List.Select(r => new TopicViewModel
            {
                Id = r.Id,
                NodeId = r.Node.Id,
                NodeName = r.Node.Name,
                //UserName = r.User.UserName,
                //Avatar = r.User.Avatar,
                Title = r.Title,
                Top = r.Top,
                Type = r.Type,
                ReplyCount = r.ReplyCount,
                LastReplyTime = r.LastReplyTime,
                CreateOn = r.CreateOn
            }).ToList();
            ViewBag.PageIndex = pageindex;
            ViewBag.PageCount = result.GetPageCount();
            ViewBag.Node = node;
            ViewBag.Count = result.Total;
            return View();
        }
    }
}
