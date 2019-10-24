using MakeIt.DAL.EF;
using MakeIt.Repository.BaseRepository;
using MakeIt.Repository.Repository.Interface;
using System.Data.Entity;

namespace MakeIt.Repository.Repository
{
    public class LabelRepository : BaseRepository<Label>, ILabelRepository
    {
        public LabelRepository(DbContext context)
            : base(context)
        {

        }
    }
}
