using Microsoft.AspNetCore.Mvc;
using MovieDb.Api.Application.Interfaces;

namespace MovieDb.Api.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _svc;
        public MoviesController(IMovieService svc) { _svc = svc; }

        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int pageSize = 12, string sort = "year", string dir = "desc")
            => Ok(await _svc.GetPagedAsync(page, pageSize, sort, dir));

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var m = await _svc.GetWithReviewsAsync(id);
            return m is null ? NotFound() : Ok(m);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string? q, [FromQuery] string? genre, [FromQuery] int? yearMin, [FromQuery] int? yearMax, [FromQuery] string sort = "title", [FromQuery] string dir = "asc")
            => Ok(await _svc.SearchAsync(q, genre, yearMin, yearMax, sort, dir));
    }
}
