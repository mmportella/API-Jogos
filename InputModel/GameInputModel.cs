using System;
using System.ComponentModel.DataAnnotations;

namespace API_Jogos.InputModel
{
    public class GameInputModel
    {

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The game name must contain between 3 and 100 characters.")]
        public string gameName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "The name of the game publisher must contain between 3 and 100 characters.")]
        public string gamePublisher { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "The price must be at least $ 1.00 and at most $1000.00.")]
        public double gamePrice { get; set; }

    }
}