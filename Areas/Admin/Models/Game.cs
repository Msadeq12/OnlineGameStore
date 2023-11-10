
using Humanizer.Localisation;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Security.Policy;

namespace PROG3050_HMJJ.Areas.Admin.Models
{
    public class Game
    {

        public int gameID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int GenreID { get; set; }
        [Required]
        public string Publisher { get; set; }
        [Required]
        public virtual Genre Genre { get; set; }
        [Required]
        public int ReleaseYear { get; set; }
        [Required]
        public decimal Price { get; set; }

        public int PlatformID { get; set; }

        public virtual Platform Platform { get; set; }



        //public Blob Icon { get; set; }


    }
}
