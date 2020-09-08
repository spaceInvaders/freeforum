using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FreeForum.ViewModels;
using System.Collections.Generic;
using FreeForum.ViewModels.Post;
using FreeForum.Data;
using System.Linq;

namespace FreeForum.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPost _postService;

        public HomeController(ILogger<HomeController> logger, IPost postService)
        {
            _logger = logger;
            _postService = postService;
        }

        public IActionResult Index()
        {
            IEnumerable<PostListingModel> posts = _postService.GetAllByNew()
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

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
