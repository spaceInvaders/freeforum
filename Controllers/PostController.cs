using FreeForum.Data;
using FreeForum.ViewModels.Post;
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
                .Select(post => new PostListingModel { 
                    Id = post.Id,
                    Name = post.Title,
                    Description = post.Description
            });

            var model = new PostIndexModel
            {
                PostList = posts
            };

            return View(model);
        }
    }
}
