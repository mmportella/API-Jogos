using API_Jogos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Jogos.Repositories
{
    public class GameRepository : IGameRepository
    {

        private static Dictionary<Guid, Game> games = new Dictionary<Guid, Game>()
        {
            {Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), new Game{ gameId = Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), gameName = "Fifa 21", gamePublisher = "EA", gamePrice = 200} },
            {Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), new Game{ gameId = Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), gameName = "Fifa 20", gamePublisher = "EA", gamePrice = 190} },
            {Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), new Game{ gameId = Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), gameName = "Fifa 19", gamePublisher = "EA", gamePrice = 180} },
            {Guid.Parse("da033439-f352-4539-879f-515759312d53"), new Game{ gameId = Guid.Parse("da033439-f352-4539-879f-515759312d53"), gameName = "Fifa 18", gamePublisher = "EA", gamePrice = 170} },
            {Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), new Game{ gameId = Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), gameName = "Street Fighter V", gamePublisher = "Capcom", gamePrice = 80} },
            {Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), new Game{ gameId = Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), gameName = "Grand Theft Auto V", gamePublisher = "Rockstar", gamePrice = 190} }
        };

        public Task<List<Game>> Get(int page, int quantity)
        {
            return Task.FromResult(games.Values.Skip((page-1)*quantity).Take(quantity).ToList());
        }

        public Task<Game> Get(Guid gameId)
        {
            if (!games.ContainsKey(gameId))
                return null;
            return Task.FromResult(games[gameId]);
        }

        public Task<List<Game>> Get(string gameName, string gamePublisher)
        {
            return Task.FromResult(games.Values.Where(game => game.gameName.Equals(gameName) && game.gamePublisher.Equals(gamePublisher)).ToList());
        }

        public Task<List<Game>> NoLambdaGet(string gameName, string gamePublisher)
        {
            var result = new List<Game>();

            foreach (var game in games.Values)
            {
                if (game.gameName.Equals(gameName) && game.gameName.Equals(gamePublisher))
                    result.Add(game);
            }

            return Task.FromResult(result);
        }

        public Task Post(Game game)
        {
            games.Add(game.gameId, game);
            return Task.CompletedTask;
        }

        public Task Put(Game game)
        {
            games[game.gameId] = game;
            return Task.CompletedTask;
        }

        public Task Delete(Guid gameId)
        {
            games.Remove(gameId);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }

    }
}
