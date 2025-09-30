namespace MovieDb.Api.Domain.DTOs
{
    public record MovieDto(int Id, string Title, int Year, string Genre, double Rating);
    public record MovieWithReviewsDto(int Id, string Title, int Year, string Genre, double Rating, List<ReviewDto> Reviews);
    public record ReviewDto(int Id, string Author, string Text, int Stars, DateTime CreatedAt);
    public record PagedResult<T>(int Total, List<T> Items);
}
