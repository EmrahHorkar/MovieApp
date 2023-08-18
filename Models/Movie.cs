namespace MovieApp.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Director { get; set; }
        public string ImageUrl { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public List<UserFavoriteMovie> FavoritedBy { get; set; } = new List<UserFavoriteMovie>();
        public List<UserWatchedMovie> WatchedBy { get; set; } = new List<UserWatchedMovie>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
