using Microsoft.EntityFrameworkCore;
using MovieDb.Api.Application.Interfaces;
using MovieDb.Api.Domain.Entities;
using MovieDb.Api.Infrastructure.Data;

namespace MovieDb.Api.Infrastructure.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _db;
        public MovieRepository(AppDbContext db) { _db = db; }

        public Task<int> CountAsync() => _db.Movies.CountAsync();

        public IQueryable<Movie> Query() => _db.Movies.AsQueryable();

        public Task<Movie?> GetByIdAsync(int id) =>
            _db.Movies.Include(m => m.Reviews.OrderByDescending(r => r.CreatedAt))
                .FirstOrDefaultAsync(m => m.Id == id);
    }
}
