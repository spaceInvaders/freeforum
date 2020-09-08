using FreeForum.Data.Models;
using System.Threading.Tasks;

namespace FreeForum.Data
{
    public interface IReply
    {
        PostReply GetById(int id);
        public bool IdExists(int idToVerify);
        Task UpdateReply(int id, string editedContent);
        Task AddReply(PostReply reply);
    }
}
