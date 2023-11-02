namespace GameService.Entities
{
    public class Genre
    {
        public int GenreID { get; set; }

        public string GenreName { get; set; }

        public List<Game> Games { get; set; }
    }
}
