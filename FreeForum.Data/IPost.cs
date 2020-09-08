using FreeForum.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeForum.Data
{
    public interface IPost
    {
        Post GetById(int id);
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetAllByNew();

        Task Add(Post post);
    }
}
