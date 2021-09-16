using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API_Jogos.InputModel;
using API_Jogos.ViewModel;

namespace API_Jogos.Services
{
    public interface IGameService
    {
        Task<List<GameViewModel>> Get(int page, int quantity);
        Task<GameViewModel> Get(Guid gameId);
        Task<GameViewModel> Post(GameInputModel game);
        Task Put(Guid gameId, GameInputModel game);
        Task Patch(Guid gameId, double gamePrice);
        Task Delete(Guid gameId);
    }
}