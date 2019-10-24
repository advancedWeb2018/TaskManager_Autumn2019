using System.ComponentModel.DataAnnotations;

namespace MakeIt.DAL.Common
{
    public abstract class Entity<T> : IEntity<T>
    {
        [Key]
        public virtual T Id { get; set; }
    }
}
