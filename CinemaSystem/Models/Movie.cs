namespace CinemaSystem.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(200, ErrorMessage = "Max 200 characters")]
        public string Name { get; set; }
        [MaxLength(1000, ErrorMessage = "Max 1000 characters")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Price is required")]
        [Range(0, 10000, ErrorMessage = "Price must be between 0 and 10000")]
        public double Price { get; set; }
        [Required(ErrorMessage = "Status is required")]
        [MaxLength(50)]
        public string Status { get; set; }
        [Required(ErrorMessage = "Date is required")]
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
