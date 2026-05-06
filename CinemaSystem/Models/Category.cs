namespace CinemaSystem.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100, ErrorMessage = "Max 100 characters")]
        public string Name { get; set; }
        public List<Movie>? Movies { get; set; }
    }
}
