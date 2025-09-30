using MovieDb.Api.Domain.Entities;

namespace MovieDb.Api.Application.Interfaces
{
    public interface IMovieRepository
    {
        Task<int> CountAsync();
        IQueryable<Movie> Query();
        Task<Movie?> GetByIdAsync(int id);
    }
}
