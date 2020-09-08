using System;
using System.Security.Claims;
using System.Threading.Tasks;
using FreeForum.Data;
using FreeForum.Data.Models;
using FreeForum.ViewModels.Reply;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FreeForum.Controllers
{
    public class ReplyController : Controller
    {
        private readonly IReply _replyService;
        private readonly IPost _postService;
        private readonly UserManager<User> _userManager;

        public ReplyController(IReply replyService, IPost postService, UserManager<User> userManager)
        {
            _replyService = replyService;
            _postService = postService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult EditReply(int id)
        {
            if (_replyService.IdExists(idToVerify: id))
            {
                var reply = _replyService.GetById(id: id);
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var model = new ReplyEditModel
                {
                    ReplyId = id,
                    AuthorId = userId,
                    ContentToEdit = reply.Content,
                    PostId = reply.Post.Id.ToString(),
                    PostTitle = reply.Post.Title,
                    PostContent = reply.Post.Content
                };

                return View(model);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditReply(ReplyEditModel model)
        {
            if (ModelState.IsValid)
            {
                await _replyService.UpdateReply(id: model.ReplyId, editedContent: model.EditedContent);

                return RedirectToAction("PostDetaile", "Post", new { id = Int32.Parse(model.PostId) });
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreateReply(int id)
        {
            if (_postService.IdExists(idToVerify: id))
            {
                var post = _postService.GetById(id: id);
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                var model = new PostReplyModel
                {
                    AuthorId = user.Id,
                    AuthorName = User.Identity.Name,

                    Created = DateTime.Now,

                    PostId = post.Id,
                    PostTitle = post.Title,
                    PostContent = post.Content
                };

                return View(model);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateReply(PostReplyModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByIdAsync(model.AuthorId).Result;
                var reply = BuildReply(model, user);

                await _replyService.AddReply(reply);

                return RedirectToAction("PostDetaile", "Post", new { id = model.PostId });
            }
            else
            {
                var post = _postService.GetById(id: model.PostId);
                model.PostTitle = post.Title;

                return View(model);
            }
        }

        #region Private methods

        private PostReply BuildReply(PostReplyModel model, User user)
        {
            var post = _postService.GetById(model.PostId);

            return new PostReply
            {
                Content = model.ReplyContent,
                Created = DateTime.Now,

                UserId = model.AuthorId,
                User = user,

                Post = post
            };
        }

        #endregion
    }
}
