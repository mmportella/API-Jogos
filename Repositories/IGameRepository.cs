using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API_Jogos.Entities;

namespace API_Jogos.Repositories
{
    public interface IGameRepository : IDisposable
    {
        Task<List<Game>> Get(int page, int quantity);
        Task<Game> Get(Guid gameId);
        Task<List<Game>> Get(string gameName, string gamePublisher);
        Task Post(Game game);
        Task Put(Game game);
        Task Delete (Guid gameId);
    }
}