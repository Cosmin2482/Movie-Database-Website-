namespace MovieDb.Api.Domain.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string Author { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public int Stars { get; set; }
        public DateTime CreatedAt { get; set; }
        public Movie? Movie { get; set; }
    }
}
