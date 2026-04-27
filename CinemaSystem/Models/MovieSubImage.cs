namespace CinemaSystem.Models
{
    public class MovieSubImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
