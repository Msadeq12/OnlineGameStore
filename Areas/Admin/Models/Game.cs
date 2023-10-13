
using Humanizer.Localisation;
using System.Reflection.Metadata;
using System.Security.Policy;

namespace PROG3050_HMJJ.Areas.Admin.Models
{
    public class Game
    {
        public int gameID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int GenreID { get; set; }

        public string Publisher { get; set; }

        public virtual Genre Genre { get; set; }

        public int ReleaseYear { get; set; }

        public decimal Price { get; set; }

        //public Blob Icon { get; set; }


    }
}
