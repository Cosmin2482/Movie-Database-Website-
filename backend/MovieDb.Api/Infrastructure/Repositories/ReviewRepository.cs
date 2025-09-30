using MovieDb.Api.Application.Interfaces;
using MovieDb.Api.Domain.Entities;
using MovieDb.Api.Infrastructure.Data;

namespace MovieDb.Api.Infrastructure.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly AppDbContext _db;
        public ReviewRepository(AppDbContext db) { _db = db; }
        public async Task AddAsync(Review r) { await _db.Reviews.AddAsync(r); }
        public Task SaveChangesAsync() => _db.SaveChangesAsync();
    }
}
