namespace MovieApp.Models
{
    public class CategoryMovie
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
