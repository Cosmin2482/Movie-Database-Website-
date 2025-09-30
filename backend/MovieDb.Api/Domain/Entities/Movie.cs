namespace MovieDb.Api.Domain.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Genre { get; set; } = string.Empty;
        public double Rating { get; set; }
        public List<Review> Reviews { get; set; } = new();
    }
}
