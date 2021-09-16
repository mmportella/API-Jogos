using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using API_Jogos.InputModel;
using API_Jogos.ViewModel;
using API_Jogos.Services;
using API_Jogos.Exceptions;

namespace API_Jogos.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {

        // An instance of IGameService
        private readonly IGameService _gameService;
        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        // Return a list of all game objects.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameViewModel>>> GameGet([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int quantity = 5)
        {
            var game = await _gameService.Get(page, quantity);
            if (game.Count() == 0)
                return NoContent();
            return Ok(game);
        }

        // Return a specific game object with the provided gameId.
        [HttpGet("{gameId:guid}")]
        public async Task<ActionResult<GameViewModel>> GameGet([FromRoute] Guid gameId)
        {
            var game = await _gameService.Get(gameId);
            if (game == null)
                return NoContent();
            return Ok();
        }

        // Create a new Game.
        // Also returns the object so you can check the gameId.
        [HttpPost]
        public async Task<ActionResult<GameViewModel>> GamePost([FromBody] GameInputModel gameInputModel)
        {
            try
            {
                var game = await _gameService.Post(gameInputModel);
                return Ok(game);
            }
            catch (ExistingGameException ex)
            {
                return UnprocessableEntity("There is already a game with this name for this publisher.");
            }
        }

        // Update the game object with given gameId.
        [HttpPut("{gameId:guid}")]
        public async Task<ActionResult> GamePut([FromRoute] Guid gameId, [FromBody] GameInputModel gameInputModel)
        {
            try
            {
                await _gameService.Put(gameId, gameInputModel);
                return Ok();
            }
            catch (NonExistingGameException ex)
            {
                return NotFound("This game doesn't exist.");
            }
        }

        // Update the game price in object with a given gameId.
        [HttpPatch("{gameId:guid}/gamePrice/{gamePrice:double}")]
        public async Task<ActionResult> GamePatch([FromRoute] Guid gameId, [FromRoute] double gamePrice)
        {
            try
            {
                await _gameService.Patch(gameId, gamePrice);
                return Ok();
            }
            catch (NonExistingGameException ex)
            {
                return NotFound("This game doesn't exist.");
            }
        }

        // Delete the game object with the given gameId.
        [HttpDelete("{gameId:guid}")]
        public async Task<ActionResult> GameDelete([FromRoute] Guid gameId)
        {
            try
            {
                await _gameService.Delete(gameId);
                return Ok();
            }
            catch (NonExistingGameException ex)
            {
                return NotFound("This game doesn't exist.");
            }
        }

    }
}