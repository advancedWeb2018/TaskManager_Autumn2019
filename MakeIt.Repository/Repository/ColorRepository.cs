using MakeIt.DAL.EF;
using MakeIt.Repository.BaseRepository;
using System.Data.Entity;

namespace MakeIt.Repository.Repository
{
    public interface IColorRepository : IGenericRepository<Color>
    {
    }

    public class ColorRepository : GenericRepository<Color>, IColorRepository
    {
        public ColorRepository(DbContext context)
            : base(context)
        {

        }
    }
}
