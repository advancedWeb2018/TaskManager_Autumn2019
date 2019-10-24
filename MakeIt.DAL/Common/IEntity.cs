namespace MakeIt.DAL.Common
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
