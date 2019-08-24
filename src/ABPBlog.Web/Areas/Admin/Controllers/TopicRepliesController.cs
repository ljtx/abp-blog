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
    public class TopicRepliesController: ABPBlogControllerBase
    {
        private IRepository<TopicReply> _topicReplyRepository;
        private IRepository<Topic> _topicRepository;
        public TopicRepliesController(IRepository<TopicReply> topicReplyRepository, IRepository<Topic> topicRepository)
        {
            _topicReplyRepository = topicReplyRepository;
            _topicRepository = topicRepository;
        }

        // GET: TopicReplies
        public async Task<IActionResult> Index(int id)
        {
            var topicreplys = _topicReplyRepository.GetAllListAsync(r => r.TopicId == id);
            return View(await topicreplys);
        }
        // GET: TopicReplies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topicReply = await _topicReplyRepository.GetAsync(id.Value);
            if (topicReply == null)
            {
                return NotFound();
            }
            var topic = await _topicRepository.GetAsync(topicReply.TopicId);
            topic.ReplyCount -= 1;
            _topicRepository.Update(topic);
            _topicReplyRepository.Delete(topicReply);
            return NoContent();
        }
    }
}
