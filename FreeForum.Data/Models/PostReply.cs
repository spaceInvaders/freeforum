using System;

namespace FreeForum.Data.Models
{
    public class PostReply
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public Post Post { get; set; }
    }
}
