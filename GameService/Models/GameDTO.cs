using GameService.Entities;
using System.Text.Json.Serialization;

namespace GameService.Models
{
    public class GameDTO
    {
        public int ID { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public string? Publisher { get; set; }

        public int? ReleaseYear { get; set; }

        public string? GameGenre { get; set; }

        public string? GamePlatform { get; set; }


    }
}
