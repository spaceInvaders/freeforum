using System;
using System.ComponentModel.DataAnnotations;

namespace FreeForum.ViewModels.Reply
{
    public class PostReplyModel
    {
        public int Id { get; set; }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }

        public DateTime Created { get; set; }

        [Required]
        public string ReplyContent { get; set; }

        public int PostId { get; set; }
        public string PostTitle { get; set; }
        public string PostContent { get; set; }

    }
}
