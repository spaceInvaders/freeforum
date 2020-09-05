using FreeForum.ViewModels.Reply;
using System;
using System.Collections.Generic;

namespace FreeForum.ViewModels.Post
{
    public class PostDetaileModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public DateTime Created { get; set; }
        public string PostContent { get; set; }

        public IEnumerable<PostReplyModel> Replies { get; set; }
    }
}
