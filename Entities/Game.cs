using System;

namespace API_Jogos.Entities
{
    public class Game
    {
        public Guid gameId { get; set; }
        public string gameName { get; set; }
        public string gamePublisher { get; set; }
        public double gamePrice { get; set; }
    }
}