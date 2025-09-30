using MovieDb.Api.Domain.DTOs;

namespace MovieDb.Api.Application.Interfaces
{
    public interface IMovieService
    {
        Task<PagedResult<MovieDto>> GetPagedAsync(int page, int pageSize, string sort, string dir);
        Task<IEnumerable<MovieDto>> SearchAsync(string? q, string? genre, int? yearMin, int? yearMax, string sort, string dir);
        Task<MovieWithReviewsDto?> GetWithReviewsAsync(int id);
    }
}
