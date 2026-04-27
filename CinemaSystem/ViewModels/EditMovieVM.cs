
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
namespace CinemaSystem.ViewModels
{
    public class EditMovieVM
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

        public string? ExistingMainImg { get; set; }

        public IFormFile? MainImg { get; set; }

        public List<IFormFile>? SubImages { get; set; }

        public int CategoryId { get; set; }
        public int CinemaId { get; set; }

        public List<int>? ActorIds { get; set; }

        public List<int>? ExistingActorIds { get; set; }
    }
}