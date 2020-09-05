using FreeForum.Data;
using FreeForum.Data.Models;
using FreeForum.ViewModels.Post;
using FreeForum.ViewModels.Reply;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeForum.Controllers
{
    public class PostController : Controller
    {
        private IPost PostService { get; }
        private static UserManager<User> UserManager { get; set; }

        public PostController(IPost postService, UserManager<User> userManager)
        {
            PostService = postService;
            UserManager = userManager;
        }

        public IActionResult Index()
        {
            IEnumerable<PostListingModel> posts = PostService.GetAll()
                .Select(post => new PostListingModel
                { 
                    Id = post.Id,
                    Title = post.Title,
                    UserName = post.User.UserName,
                    DateCreated = post.Created.ToString(),
                    Content = post.Content,
                    RepliesCount = post.Replies.Count()
                });

            var model = new PostIndexModel
            {
                PostList = posts
            };

            return View(model);
        }
        public IActionResult PostDetaile(int id)
        {
            var post = PostService.GetById(id: id);

            var replies = BuildPostReplies(replies: post.Replies);

            var model = new PostDetaileModel
            {
                Id = post.Id,
                Title = post.Title,
                AuthorId = post.User.Id,
                AuthorName = post.User.UserName,
                Created = post.Created,
                PostContent = post.Content,
                Replies = replies
            };

            return View(model);
        }

        public IActionResult Create()
        {
            var model = new NewPostModel
            {
                AuthorName = User.Identity.Name
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddPost(NewPostModel model)
        {
            var userId = UserManager.GetUserId(User);
            var user = UserManager.FindByIdAsync(userId).Result;
            var post = BuildPost(model, user);

            await PostService.Add(post);

            return RedirectToAction("PostDetaile", "Post", new { post.Id });
        }

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
            return replies.Select(reply => new PostReplyModel
            {
                Id = reply.Id,
                AuthorName = reply.User.UserName,
                AuthorId = reply.UserId,
                Created = reply.Created,
                ReplyContent = reply.Content,
                PostId = reply.PostId
            });
        }
    }
}
