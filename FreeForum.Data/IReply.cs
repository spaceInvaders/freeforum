using FreeForum.Data.Models;
using System.Threading.Tasks;

namespace FreeForum.Data
{
    public interface IReply
    {
        PostReply GetById(int id);
        Task UpdateReply(int id, string editedContent);
        Task AddReply(PostReply reply);
    }
}
