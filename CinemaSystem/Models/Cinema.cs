namespace CinemaSystem.Models
{
    public class Cinema
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string? Location { get; set; }
        public List<Movie>? Movies { get; set; }
    }
}
