namespace MovieApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CategoryMovie> CategoryMovies { get; set; } = new List<CategoryMovie>();
    }
}
