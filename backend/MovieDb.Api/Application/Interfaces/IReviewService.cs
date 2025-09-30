using MovieDb.Api.Domain.DTOs;

namespace MovieDb.Api.Application.Interfaces
{
    public interface IReviewService
    {
        Task<ReviewDto?> AddAsync(int movieId, string author, CreateReviewDto dto);
    }
}
