using Microsoft.EntityFrameworkCore;
using Movies.Core.DomainContracts;
using Movies.Core.Models.Entities;
using Movies.Data.Data;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Movies.Data.Repositories
{
    public abstract class Repository<T> : IRepository<T>, IInternalRepository<T> where T : Entity
    {
        protected readonly MovieApiContext Context;

        protected DbSet<T> Db { get; }

        public Repository(MovieApiContext movieApiContext)
        {
            Context = movieApiContext;
            Db = movieApiContext.Set<T>();
        }

        public IQueryable<T> FindAll() => Db;

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) => Db.Where(expression);

        public void Add(T entity) => Db.Add(entity);

        public void Delete(T entity) => Db.Remove(entity);

        public void Update(T entity) => Db.Update(entity);
    }
}
