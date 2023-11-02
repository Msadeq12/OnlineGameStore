using System.ComponentModel.DataAnnotations;

namespace PROG3050_HMJJ.Areas.Admin.Models
{
    public class GamesViewModel
    {
        public int ID { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string? Publisher { get; set; }

        [Required]
        public int? ReleaseYear { get; set; }

        [Required]
        public string? GameGenre { get; set; }

        [Required]
        public string? GamePlatform { get; set; }

    }
}
