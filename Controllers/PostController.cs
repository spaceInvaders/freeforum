using FreeForum.Data;
using FreeForum.Data.Models;
using FreeForum.ViewModels.Post;
using FreeForum.ViewModels.Reply;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FreeForum.Controllers
{
    public class PostController : Controller
    {
        private IPost PostService { get; }
        public PostController(IPost postService)
        {
            PostService = postService;
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
                    Description = post.Description,
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
