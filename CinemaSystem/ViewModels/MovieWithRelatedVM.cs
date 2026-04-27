using CinemaSystem.Models;

namespace CinemaSystem.ViewModels
{
    public class MovieWithRelatedVM
    {
        public IEnumerable<Movie> Movies { get; set; } = [];
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
