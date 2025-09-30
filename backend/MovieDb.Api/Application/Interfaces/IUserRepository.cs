using MovieDb.Api.Domain.Entities;

namespace MovieDb.Api.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> FindByUsernameAsync(string username);
        Task AddAsync(User user);
        Task SaveChangesAsync();
    }
}
