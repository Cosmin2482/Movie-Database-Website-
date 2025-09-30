using Microsoft.EntityFrameworkCore;
using MovieDb.Api.Domain.Entities;

namespace MovieDb.Api.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Movie> Movies => Set<Movie>();
        public DbSet<Review> Reviews => Set<Review>();
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Movie>().HasIndex(m => m.Title);
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Movie).WithMany(m => m.Reviews).HasForeignKey(r => r.MovieId);
        }
    }

    public static class DbSeeder
    {
        static readonly string[] Genres = { "Action","Adventure","Animation","Comedy","Crime","Drama","Fantasy","Horror","Mystery","Romance","Sci-Fi","Thriller" };
        public static void Seed(AppDbContext db)
        {
            if (!db.Users.Any())
            {
                db.Users.Add(new User { Username="demo", PasswordHash=BCrypt.Net.BCrypt.HashPassword("demo123") });
                db.SaveChanges();
            }
            if (db.Movies.Any()) return;

            var rnd = new Random(42);
            var titles = new HashSet<string>();
            string RandomTitle()
            {
                string[] adj = { "Silent","Golden","Neon","Broken","Hidden","Eternal","Burning","Infinite","Crimson","Quantum","Shadow","Fading","Radiant","Midnight","Forgotten" };
                string[] nouns = { "Dreams","Empire","Voices","Horizons","Echoes","Odyssey","Legacy","Storm","Labyrinth","Frontier","Pulse","Mirage","Requiem","Chronicles","Code" };
                return $"{adj[rnd.Next(adj.Length)]} {nouns[rnd.Next(nouns.Length)]}" + (rnd.Next(0,5)==0 ? " " + rnd.Next(2,8) : "");
            }
            var list = new List<Movie>();
            for (int i=0; i<1200; i++)
            {
                var title = RandomTitle();
                while (!titles.Add(title)) title += " " + rnd.Next(2,99);
                list.Add(new Movie
                {
                    Title = title,
                    Year = rnd.Next(1970, 2025),
                    Genre = Genres[rnd.Next(Genres.Length)],
                    Rating = Math.Round(rnd.NextDouble()*4 + 6, 1)
                });
            }
            db.Movies.AddRange(list);
            db.SaveChanges();
        }
    }
}
