namespace CinemaSystem.Models
{
    public class Cinema
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100, ErrorMessage = "Max 100 characters")]
        public string Name { get; set; }
        [MaxLength(200, ErrorMessage = "Max 200 characters")]

        public string? Location { get; set; }
        public List<Movie>? Movies { get; set; }
    }
}
