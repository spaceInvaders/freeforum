using System;

namespace FreeForum.ViewModels.Reply
{
    public class PostReplyModel
    {
        public int Id { get; set; }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public DateTime Created { get; set; }
        public string ReplyContent { get; set; }

        public string PostId { get; set; }
    }
}
