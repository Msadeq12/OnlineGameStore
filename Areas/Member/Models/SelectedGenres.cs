﻿using PROG3050_HMJJ.Models;
using System.ComponentModel.DataAnnotations;


namespace PROG3050_HMJJ.Areas.Member.Models
{
    public class SelectedGenres
    {
        [Key]
        public int ID { get; set; }


        public virtual Preferences Preferences { get; set; }


        public virtual Genres Genres { get; set; }
    }
}
