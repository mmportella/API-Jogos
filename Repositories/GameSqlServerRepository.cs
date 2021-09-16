using API_Jogos.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace API_Jogos.Repositories
{
    public class GameSqlServerRepository : IGameRepository
    {

        private readonly SqlConnection sqlConnection;
        public GameSqlServerRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task<List<Game>> Get(int page, int quantity)
        {
            var games = new List<Game>();
            var comando = $"select * from Games order by gameId offset {((page - 1) * quantity)} rows fetch next {quantity} rows only";
            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();
            while (sqlDataReader.Read())
            {
                games.Add(new Game
                {
                    gameId = (Guid)sqlDataReader["gameId"],
                    gameName = (string)sqlDataReader["gameName"],
                    gamePublisher = (string)sqlDataReader["gamePublisher"],
                    gamePrice = (double)sqlDataReader["gamePrice"]
                });
            }
            await sqlConnection.CloseAsync();
            return games;
        }

        public async Task<Game> Get(Guid gameId)
        {
            Game game = null;
            var command = $"select * from Games where gameId = '{gameId}'";
            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();
            while (sqlDataReader.Read())
            {
                game = new Game
                {
                    gameId = (Guid)sqlDataReader["gameId"],
                    gameName = (string)sqlDataReader["gameName"],
                    gamePublisher = (string)sqlDataReader["gamePublisher"],
                    gamePrice = (double)sqlDataReader["gamePrice"]
                };
            }
            await sqlConnection.CloseAsync();
            return game;
        }

        public async Task<List<Game>> Get(string gameName, string gamePublisher)
        {
            var games = new List<Game>();

            var comando = $"select * from Games where gameName = '{gameName}' and gamePublisher = '{gamePublisher}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                games.Add(new Game
                {
                    gameId = (Guid)sqlDataReader["gameId"],
                    gameName = (string)sqlDataReader["gameName"],
                    gamePublisher = (string)sqlDataReader["gamePublisher"],
                    gamePrice = (double)sqlDataReader["gamePrice"]
                });
            }

            await sqlConnection.CloseAsync();

            return games;
        }

        public async Task Post(Game game)
        {
            var comando = $"insert Games (gameId, gameName, gamePublisher, gamePrice) values ('{game.gameId}', '{game.gameName}', '{game.gamePublisher}', {game.gamePrice.ToString().Replace(",", ".")})";
            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Put(Game game)
        {
            var command = $"update Games set gameName = '{game.gameName}', Produtora = '{game.gamePublisher}', Preco = {game.gamePrice.ToString().Replace(",", ".")} where Id = '{game.gameId}'";
            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Delete(Guid gameId)
        {
            var command = $"delete from Games where gameId = '{gameId}'";
            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public void Dispose()
        {
            sqlConnection?.Close();
            sqlConnection?.Dispose();
        }

    }
}
