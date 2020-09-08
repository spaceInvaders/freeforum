using FreeForum.Data;
using FreeForum.Data.Models;
using FreeForum.ViewModels.Post;
using FreeForum.ViewModels.Reply;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FreeForum.Controllers
{
    public class PostController : Controller
    {
        private readonly IPost _postService;
        private readonly UserManager<User> _userManager;

        public PostController(IPost postService, UserManager<User> userManager)
        {
            _postService = postService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult PostDetaile(int id)
        {
            if (_postService.IdExists(idToVerify: id))
            {
                var post = _postService.GetById(id: id);
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var replies = BuildPostReplies(replies: post.Replies);

                var model = new PostDetaileModel
                {
                    Id = post.Id,
                    Title = post.Title,
                    AuthorId = post.User.Id,
                    AuthorName = post.User.UserName,
                    Created = post.Created,
                    PostContent = post.Content,
                    Replies = replies,
                    CurrentLoggedInUserId = userId
                };

                return View(model);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult CreatePost()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(NewPostModel model)
        {
            if (!String.IsNullOrEmpty(model.Title) && model.Title.Length > 30)
            {
                ModelState.AddModelError("Title", "Too long title..");
            }
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                var user = _userManager.FindByIdAsync(userId).Result;
                var post = BuildPost(model, user);

                await _postService.Add(post);

                return RedirectToAction("PostDetaile", "Post", new { post.Id });
            }
            else
            {
                return View(model);
            }
        }

        #region Private methods

        private Post BuildPost(NewPostModel model, User user)
        {
            return new Post
            {
                Title = model.Title,
                Content = model.Content,
                Created = DateTime.Now,
                User = user
            };
        }

        private IEnumerable<PostReplyModel> BuildPostReplies(IEnumerable<PostReply> replies)
        {
            var postRepliesModels = replies.Select(reply => new PostReplyModel
            {
                Id = reply.Id,
                AuthorName = reply.User.UserName,
                AuthorId = reply.UserId,
                Created = reply.Created,
                ReplyContent = reply.Content,
                PostId = reply.Post.Id
            });

            var sortedByNewpostRepliesModels = postRepliesModels.OrderByDescending(reply => reply.Created);

            return sortedByNewpostRepliesModels;
        }

        #endregion
    }
}
