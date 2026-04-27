namespace CinemaSystem.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        [MaxLength(50)]
        public string Status { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        public string? MainImg { get; set; }


        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int CinemaId { get; set; }
        public Cinema Cinema { get; set; }
        public List<MovieActor>? MovieActors { get; set; }
        public List<MovieSubImage>? MovieSubImages { get; set; }
    }
}
