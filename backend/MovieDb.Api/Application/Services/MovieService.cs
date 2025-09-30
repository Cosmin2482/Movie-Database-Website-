using Microsoft.EntityFrameworkCore;
using MovieDb.Api.Application.Interfaces;
using MovieDb.Api.Domain.DTOs;

namespace MovieDb.Api.Application.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _repo;
        public MovieService(IMovieRepository repo) { _repo = repo; }

        public async Task<PagedResult<MovieDto>> GetPagedAsync(int page, int pageSize, string sort, string dir)
        {
            var total = await _repo.CountAsync();
            var query = _repo.Query();
            query = (sort, dir.ToLower()) switch
            {
                ("title","asc") => query.OrderBy(m => m.Title),
                ("title","desc") => query.OrderByDescending(m => m.Title),
                ("rating","asc") => query.OrderBy(m => m.Rating),
                ("rating","desc") => query.OrderByDescending(m => m.Rating),
                ("year","asc") => query.OrderBy(m => m.Year),
                _ => query.OrderByDescending(m => m.Year)
            };

            var items = await query.Skip((page-1)*pageSize).Take(pageSize)
                .Select(m => new MovieDto(m.Id, m.Title, m.Year, m.Genre, m.Rating))
                .ToListAsync();

            return new PagedResult<MovieDto>(total, items);
        }

        public async Task<IEnumerable<MovieDto>> SearchAsync(string? q, string? genre, int? yearMin, int? yearMax, string sort, string dir)
        {
            var query = _repo.Query();
            if (!string.IsNullOrWhiteSpace(q)) query = query.Where(m => m.Title.Contains(q));
            if (!string.IsNullOrWhiteSpace(genre)) query = query.Where(m => m.Genre == genre);
            if (yearMin.HasValue) query = query.Where(m => m.Year >= yearMin);
            if (yearMax.HasValue) query = query.Where(m => m.Year <= yearMax);

            query = (sort, dir.ToLower()) switch
            {
                ("title","desc") => query.OrderByDescending(m => m.Title),
                ("rating","asc") => query.OrderBy(m => m.Rating),
                ("rating","desc") => query.OrderByDescending(m => m.Rating),
                ("year","asc") => query.OrderBy(m => m.Year),
                ("year","desc") => query.OrderByDescending(m => m.Year),
                _ => query.OrderBy(m => m.Title)
            };

            return await query.Select(m => new MovieDto(m.Id, m.Title, m.Year, m.Genre, m.Rating)).ToListAsync();
        }

        public async Task<MovieWithReviewsDto?> GetWithReviewsAsync(int id)
        {
            var m = await _repo.GetByIdAsync(id);
            if (m is null) return null;
            return new MovieWithReviewsDto(
                m.Id, m.Title, m.Year, m.Genre, m.Rating,
                m.Reviews.Select(r => new ReviewDto(r.Id, r.Author, r.Text, r.Stars, r.CreatedAt)).ToList()
            );
        }
    }
}
