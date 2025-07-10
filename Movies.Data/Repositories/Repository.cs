using Microsoft.EntityFrameworkCore;
using Movies.Core.DomainContracts;
using Movies.Data.Data;

namespace Movies.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbSet<T> Db { get; }

        public Repository(MovieApiContext movieApiContext)
        {
            Db = movieApiContext.Set<T>();
        }

        public void Add(T entity) => Db.Add(entity);

        public void Delete(T entity) => Db.Remove(entity);

        public void Update(T entity) => Db.Update(entity);
    }
}
