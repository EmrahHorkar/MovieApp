namespace MovieApp.Models
{
    public class UserWatchedMovie
    {
        public int UserId { get; set; }
        public AppUser User { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
