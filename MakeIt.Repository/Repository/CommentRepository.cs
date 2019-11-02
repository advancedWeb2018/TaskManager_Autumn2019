using MakeIt.DAL.EF;
using MakeIt.Repository.BaseRepository;
using System.Data.Entity;

namespace MakeIt.Repository.Repository
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
    }
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(DbContext context)
            : base(context)
        {

        }
    }
}
