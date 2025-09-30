using Microsoft.EntityFrameworkCore;
using MovieDb.Api.Application.Interfaces;
using MovieDb.Api.Domain.Entities;
using MovieDb.Api.Infrastructure.Data;

namespace MovieDb.Api.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;
        public UserRepository(AppDbContext db) { _db = db; }

        public Task<User?> FindByUsernameAsync(string username) =>
            _db.Users.FirstOrDefaultAsync(u => u.Username == username);

        public async Task AddAsync(User user) => await _db.Users.AddAsync(user);
        public Task SaveChangesAsync() => _db.SaveChangesAsync();
    }
}
