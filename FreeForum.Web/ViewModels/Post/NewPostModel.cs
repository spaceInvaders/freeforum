using System.ComponentModel.DataAnnotations;

namespace FreeForum.ViewModels.Post
{
    public class NewPostModel
    {
        public string AuthorName { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
