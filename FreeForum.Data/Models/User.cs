using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace FreeForum.Data.Models
{
    public class User : IdentityUser
    {
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<PostReply> Replies { get; set; }
        public User()
        {
            Posts = new List<Post>();
            Replies = new List<PostReply>();
        }
    }
}
