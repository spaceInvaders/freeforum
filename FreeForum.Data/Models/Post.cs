using System;
using System.Collections.Generic;

namespace FreeForum.Data.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public List<PostReply> Replies { get; set; }
        public Post()
        {
            Replies = new List<PostReply>();
        }

        public User User { get; set; }
    }
}
