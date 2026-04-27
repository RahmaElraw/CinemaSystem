namespace CinemaSystem.Models
{
    public class Actor
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public string Image { get; set; }
        public List<MovieActor>? MovieActors { get; set; }
    }
}
