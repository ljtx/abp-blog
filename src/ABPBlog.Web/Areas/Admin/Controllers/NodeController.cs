using Abp.Domain.Repositories;
using ABPBlog.Entity;
using ABPBlog.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABPBlog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NodeController: ABPBlogAdminBaseController
    {
        private IRepository<TopicNode> _topicNodeRepository;
        public NodeController(IRepository<TopicNode> topicNodeRepository)
        {
            _topicNodeRepository = topicNodeRepository;
        }
        // GET: Node
        public ActionResult Index()
        {
            var nodes = _topicNodeRepository.GetAllList();
            return View(nodes);
        }

        // GET: Node/Create
        public ActionResult Create(int parentid)
        {
            ViewBag.ParentId = parentid;
            return View();
        }

        // POST: Node/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TopicNode node)
        {
            try
            {
                node.CreateOn = DateTime.Now;
                _topicNodeRepository.Insert(node);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Node/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Node/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Node/Delete/5
        public ActionResult Delete(int id)
        {
            var topicnode = _topicNodeRepository.GetAll().SingleOrDefault(m => m.Id == id);
            if (topicnode == null)
            {
                return NotFound();
            }
            var childnodes = _topicNodeRepository.GetAllList().Any(r => r.ParentId == id);
            if (childnodes) return Content("存在子节点");
            _topicNodeRepository.Delete(topicnode);
            return RedirectToAction("Index");
        }
    }
}
