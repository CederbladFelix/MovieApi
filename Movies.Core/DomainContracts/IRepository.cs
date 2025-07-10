namespace Movies.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}