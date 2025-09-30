using MovieDb.Api.Application.Interfaces;
using MovieDb.Api.Domain.DTOs;
using MovieDb.Api.Domain.Entities;

namespace MovieDb.Api.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _repo;
        private readonly IMovieRepository _movies;
        public ReviewService(IReviewRepository repo, IMovieRepository movies) { _repo = repo; _movies = movies; }

        public async Task<ReviewDto?> AddAsync(int movieId, string author, CreateReviewDto dto)
        {
            var movie = await _movies.GetByIdAsync(movieId);
            if (movie is null) return null;
            var entity = new Review
            {
                MovieId = movieId,
                Author = author,
                Text = dto.Text,
                Stars = Math.Clamp(dto.Stars, 1, 5),
                CreatedAt = DateTime.UtcNow
            };
            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();
            return new ReviewDto(entity.Id, entity.Author, entity.Text, entity.Stars, entity.CreatedAt);
        }
    }
}
