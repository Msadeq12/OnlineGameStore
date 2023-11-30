using PROG3050_HMJJ.Areas.Admin.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROG3050_HMJJ.Areas.Member.Models
{
    public class WishListItems
    {
        [Key]
        public int ID { get; set; }

        
        public int GameID { get; set; }


        [NotMapped]
        public virtual GamesViewModel Game {  get; set; }


        public virtual WishLists WishLists { get; set; }
    }
}
