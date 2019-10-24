using MakeIt.Repository.Repository.Interface;

namespace MakeIt.Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        IColorRepository Colors { get; }
        ICommentRepository Comments { get; }
        ILabelRepository Labels { get; }
        IMilestoneRepository Milestones { get; }
        IPriorityRepository Priorities { get; }
        IStatusRepository Statuses { get; }
        ITaskRepository Tasks { get; }
        IUserRepository Users { get; }

        void Save();
    }
}
