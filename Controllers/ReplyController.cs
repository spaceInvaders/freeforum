using System;
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
        private readonly IPost _postService;
        private readonly UserManager<User> _userManager;

        public ReplyController(IPost postService, UserManager<User> userManager)
        {
            _postService = postService;
            _userManager = userManager;
        }
        public async Task<IActionResult> Create(int id)
        {
            var post = _postService.GetById(id: id);
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var model = new PostReplyModel
            {
                AuthorId = user.Id,
                AuthorName = User.Identity.Name,

                Created = DateTime.Now,

                PostId = post.Id.ToString(),
                PostTitle = post.Title,
                PostContent = post.Content 
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddReply(PostReplyModel model)
        {
            var user = _userManager.FindByIdAsync(model.AuthorId).Result;
            var reply = BuildReply(model, user);

            await _postService.AddReply(reply);

            return RedirectToAction("PostDetaile", "Post", new { id = Int32.Parse(model.PostId) });
        }

        private PostReply BuildReply(PostReplyModel model, User user)
        {
            var post = _postService.GetById(Int32.Parse(model.PostId));

            return new PostReply
            {
                Content = model.ReplyContent,
                Created = DateTime.Now,

                UserId = model.AuthorId,
                User = user,

                Post = post,
                PostId = post.Id.ToString()
            };
        }
    }
}
