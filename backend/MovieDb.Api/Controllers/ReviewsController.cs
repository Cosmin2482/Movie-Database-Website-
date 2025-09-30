using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieDb.Api.Application.Interfaces;
using MovieDb.Api.Domain.DTOs;

namespace MovieDb.Api.Controllers
{
    [ApiController]
    [Route("api/movies/{movieId:int}/reviews")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _svc;
        public ReviewsController(IReviewService svc) { _svc = svc; }

        [HttpPost, Authorize]
        public async Task<IActionResult> Create(int movieId, [FromBody] CreateReviewDto dto)
        {
            var author = User?.Identity?.Name ?? "User";
            var review = await _svc.AddAsync(movieId, author, dto);
            return review is null ? NotFound() : Created("", review);
        }
    }
}
