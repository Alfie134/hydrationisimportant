namespace Repositories.Interfaces
{
    public interface IUpdateRepository<T>
    {
        void Update(T entity);
    }
}
