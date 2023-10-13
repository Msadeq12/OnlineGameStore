using Microsoft.AspNetCore.Identity;
using PROG3050_HMJJ.Models;
using System.ComponentModel.DataAnnotations;


namespace PROG3050_HMJJ.Areas.Admin.Models
{
    public class Games
    {
        [Key]
        public int ID { get; set; }


        public string Title { get; set; }


        public string Description { get; set; }


        public double Price { get; set; }


        public string GameFilePath { get; set; }


        public string Type { get; set; }


        public string Publisher { get; set; }


        public DateTime ReleaseDate { get; set; }


        public virtual IdentityUser IdentityUser { get; set; }


        public virtual Genres Genres { get; set; }


        public virtual Platforms Platforms { get; set; }
    }
}
