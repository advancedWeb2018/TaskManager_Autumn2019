using MakeIt.DAL.EF;
using MakeIt.Repository.BaseRepository;
using MakeIt.Repository.Repository.Interface;
using System.Data.Entity;

namespace MakeIt.Repository.Repository
{
    public class ColorRepository : BaseRepository<Color>, IColorRepository
    {
        public ColorRepository(DbContext context)
            : base(context)
        {

        }
    }
}
