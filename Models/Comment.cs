namespace MovieApp.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public int MovieId { get; set; } 
        public Movie Movie { get; set; } 

        public int UserId { get; set; } 
        public AppUser User { get; set; } 
    }
}
