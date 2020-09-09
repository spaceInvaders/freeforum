using FreeForum.Data;
using FreeForum.Data.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FreeForum.Service
{
    public class ReplyService : IReply
    {
        private readonly ApplicationDbContext _context;

        public ReplyService(ApplicationDbContext context)
        {
            _context = context;
        }

        public PostReply GetById(int id)
        {
            var reply = _context.PostReplies
                .Where(reply => reply.Id == id)
                .Include(reply => reply.Post)
                .Include(reply => reply.User)
                .First();

            return reply;
        }
        public bool IdExists(int idToVerify)
        {
            return _context.PostReplies.Any(post => post.Id == idToVerify);
        }

        public async Task UpdateReply(int id, string editedContent)
        {
            var reply = GetById(id: id);
            if (reply != null)
            {
                reply.Content = editedContent;

                _context.PostReplies.Update(reply);
            }

            await _context.SaveChangesAsync();
        }

        public async Task AddReply(PostReply reply)
        {
            _context.PostReplies.Add(reply);

            await _context.SaveChangesAsync();
        }
    }
}
