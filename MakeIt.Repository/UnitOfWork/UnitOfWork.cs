using System;
using MakeIt.DAL.EF;
using MakeIt.Repository.Repository;
using MakeIt.Repository.Repository.Interface;

namespace MakeIt.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public readonly MakeItContext _context = new MakeItContext();

        public IColorRepository Colors { get; private set; }
        public ICommentRepository Comments { get; private set; }
        public ILabelRepository Labels { get; private set; }
        public IMilestoneRepository Milestones { get; private set; }
        public IPriorityRepository Priorities { get; private set; }
        public IStatusRepository Statuses { get; private set; }
        public ITaskRepository Tasks { get; private set; }
        public IUserRepository Users { get; private set; }

        public UnitOfWork()
        {
            Colors = new ColorRepository(_context);
            Comments = new CommentRepository(_context);
            Labels = new LabelRepository(_context);
            Milestones = new MilestoneRepository(_context);
            Priorities = new PriorityRepository(_context);
            Statuses = new StatusRepository(_context);
            Tasks = new TaskRepository(_context);
            Users = new UserRepository(_context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
