using MovieDb.Api.Domain.Entities;

namespace MovieDb.Api.Application.Interfaces
{
    public interface IReviewRepository
    {
        Task AddAsync(Review r);
        Task SaveChangesAsync();
    }
}
