using Microsoft.EntityFrameworkCore;
using MoviesGallery.Context.Seeder;
using MoviesGallery.Models;

namespace MoviesGallery.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<DownloadLink> DownloadLinks { get; set; }
        public DbSet<FilmStar> FilmStars { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Quality> Qualities { get; set; }
        public DbSet<Screenshot> Screenshots { get; set; }
        public DbSet<SlideShow> SlideShows { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Seed();
        }
    }
}
