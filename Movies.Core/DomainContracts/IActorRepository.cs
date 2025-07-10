using Movies.Core.Models.Entities;

namespace Movies.Data.Repositories
{
    public interface IActorRepository
    {
        Task<bool> AnyAsync(int id);
        Task<Actor?> GetAsync(int id);
        Task<IEnumerable<Actor>> GetAllAsync();
    }
}