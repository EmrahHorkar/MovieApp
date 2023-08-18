namespace MovieApp.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ImageUrl { get; set; }
        // Navigation property for the Author relationship
        public AppUser Author { get; set; }

        // Foreign key property for the Author relationship
        public int AuthorId { get; set; } // Assuming AuthorId is an int based on your AppUser class

        // Other properties and methods
    }
}
