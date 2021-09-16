using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Jogos.Entities;
using API_Jogos.Exceptions;
using API_Jogos.Repositories;
using API_Jogos.InputModel;
using API_Jogos.ViewModel;

namespace API_Jogos.Services
{
    public class GameService : IGameService
    {

        private readonly IGameRepository _gameRepository;
        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<List<GameViewModel>> Get(int page, int quantity)
        {
            var games = await _gameRepository.Get(page, quantity);
            return games.Select(game => new GameViewModel
            {
                gameId = game.gameId,
                gameName = game.gameName,
                gamePublisher = game.gamePublisher,
                gamePrice = game.gamePrice
            }).ToList();
        }

        public async Task<GameViewModel> Get(Guid gameId)
        {
            var game = await _gameRepository.Get(gameId);
            if (game == null)
                return null;
            return new GameViewModel
            {
                gameId = game.gameId,
                gameName = game.gameName,
                gamePublisher = game.gamePublisher,
                gamePrice = game.gamePrice
            };
        }

        public async Task<GameViewModel> Post(GameInputModel game)
        {
            var gameEntity = await _gameRepository.Get(game.gameName, game.gamePublisher);
            if (gameEntity.Count > 0)
                throw new ExistingGameException();

            var gamePost = new Game
            {
                gameId = Guid.NewGuid(),
                gameName = game.gameName,
                gamePublisher = game.gamePublisher,
                gamePrice = game.gamePrice
            };
            await _gameRepository.Post(gamePost);

            return new GameViewModel
            {
                gameId = gamePost.gameId,
                gameName = game.gameName,
                gamePublisher = game.gamePublisher,
                gamePrice = game.gamePrice
            };
        }

        public async Task Put(Guid gameId, GameInputModel game)
        {
            var gameEntity = await _gameRepository.Get(gameId);
            if (gameEntity == null)
                throw new NonExistingGameException();

            gameEntity.gameName = game.gameName;
            gameEntity.gamePublisher = game.gamePublisher;
            gameEntity.gamePrice = game.gamePrice;
            await _gameRepository.Put(gameEntity);
        }

        public async Task Patch(Guid gameId, double gamePrice)
        {
            var gameEntity = await _gameRepository.Get(gameId);
            if (gameEntity == null)
                throw new NonExistingGameException();

            gameEntity.gamePrice = gamePrice;
            await _gameRepository.Put(gameEntity);
        }

        public async Task Delete(Guid gameId)
        {
            var game = _gameRepository.Get(gameId);
            if (game == null)
                throw new NonExistingGameException();

            await _gameRepository.Delete(gameId);
        }

        public void Dispose()
        {
            _gameRepository?.Dispose();
        }

    }
}