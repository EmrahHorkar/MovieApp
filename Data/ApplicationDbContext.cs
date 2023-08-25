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
        public DbSet<CategoryMovie> CategoryMovies { get; set; } // Add this line
        public DbSet<UserMessage> UserMessages { get; set; }
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

            modelBuilder.Entity<CategoryMovie>()
            .HasKey(cm => new { cm.CategoryId, cm.MovieId });

            modelBuilder.Entity<CategoryMovie>()
                .HasOne(cm => cm.Category)
                .WithMany(c => c.CategoryMovies)
                .HasForeignKey(cm => cm.CategoryId);

            modelBuilder.Entity<CategoryMovie>()
                .HasOne(cm => cm.Movie)
                .WithMany(m => m.CategoryMovies)
                .HasForeignKey(cm => cm.MovieId);
            modelBuilder.Entity<UserMessage>()
           .HasOne(um => um.Sender)
           .WithMany() // Assuming you don't want a collection navigation property on the sender
           .HasForeignKey(um => um.SenderId)
           .OnDelete(DeleteBehavior.Restrict); // Configure the delete behavior as needed

            modelBuilder.Entity<UserMessage>()
                .HasOne(um => um.Receiver)
                .WithMany() // Assuming you don't want a collection navigation property on the receiver
                .HasForeignKey(um => um.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
            // Other configurations...
            modelBuilder.Entity<UserMessage>()
   .HasKey(um => um.Id);    
            base.OnModelCreating(modelBuilder);
        }
    }
}
