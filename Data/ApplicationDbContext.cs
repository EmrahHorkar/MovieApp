using Microsoft.EntityFrameworkCore;
using MovieApp.Models;

namespace MovieApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure UserFavoriteMovie relationship
            modelBuilder.Entity<UserFavoriteMovie>()
                .HasKey(ufm => new { ufm.UserId, ufm.MovieId });

            modelBuilder.Entity<UserFavoriteMovie>()
                .HasOne(ufm => ufm.User)
                .WithMany(u => u.FavoriteMovies)
                .HasForeignKey(ufm => ufm.UserId);

            modelBuilder.Entity<UserFavoriteMovie>()
                .HasOne(ufm => ufm.Movie)
                .WithMany(m => m.FavoritedBy)
                .HasForeignKey(ufm => ufm.MovieId);

            // Configure UserWatchedMovie relationship
            modelBuilder.Entity<UserWatchedMovie>()
                .HasKey(uwm => new { uwm.UserId, uwm.MovieId });

            modelBuilder.Entity<UserWatchedMovie>()
                .HasOne(uwm => uwm.User)
                .WithMany(u => u.WatchedMovies)
                .HasForeignKey(uwm => uwm.UserId);

            modelBuilder.Entity<UserWatchedMovie>()
                .HasOne(uwm => uwm.Movie)
                .WithMany(m => m.WatchedBy)
                .HasForeignKey(uwm => uwm.MovieId);


            // Other configurations...

            base.OnModelCreating(modelBuilder);
        }
    }
}
