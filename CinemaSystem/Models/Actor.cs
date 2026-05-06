namespace CinemaSystem.Models
{
    public class Actor
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100, ErrorMessage = "Max 100 characters")]
        public string Name { get; set; }
        public string Image { get; set; }
        public List<MovieActor>? MovieActors { get; set; }
    }
}
