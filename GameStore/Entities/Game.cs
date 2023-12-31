﻿using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;

namespace GameService.Entities
{
    /// <summary>
    /// A model class for the game, with foreign keys from Genre class
    /// </summary>
    public class Game
    {
        [Key]
        public int gameID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int GenreID { get; set; }

        [JsonIgnore]
        public virtual Genre? Genre { get; set; }

        public string Publisher { get; set; }

        public string OrderType { get; set; }

        public bool isPurchased { get; set; } = false;

        public int ReleaseYear { get; set; }

        public int PlatformID { get; set; }

        [JsonIgnore]
        public virtual Platform? Platform { get; set; }

       
    }
}
