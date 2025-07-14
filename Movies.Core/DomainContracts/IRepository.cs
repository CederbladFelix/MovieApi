using Movies.Core.Models.Entities;
using System.Linq.Expressions;

namespace Movies.Core.DomainContracts
{
    public interface IRepository<T> : IInternalRepository<T> where T : Entity
    {
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}