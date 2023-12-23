using GameService.Entities;

namespace GameService.Models
{
    public class GenrePlatformDTO
    {
        public List<Platform> PlatformList { get; set; }
        public List<Genre> GenreList { get; set; }
    }
}
