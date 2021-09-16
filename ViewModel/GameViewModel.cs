using System;
namespace API_Jogos.ViewModel
{
    public class GameViewModel
    {
        public Guid gameId { get; set; }
        public string gameName { get; set; }
        public string gamePublisher { get; set; }
        public double gamePrice { get; set; }
    }
}