using MakeIt.DAL.EF;
using MakeIt.Repository.BaseRepository;
using MakeIt.Repository.Repository.Interface;
using System.Data.Entity;

namespace MakeIt.Repository.Repository
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(DbContext context)
            : base(context)
        {

        }
    }
}
