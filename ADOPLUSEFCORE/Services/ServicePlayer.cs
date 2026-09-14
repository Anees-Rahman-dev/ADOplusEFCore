using ADOPLUSEFCORE.models;
using Microsoft.Data.SqlClient;

namespace ADOPLUSEFCORE.Services
{
    public class ServicePlayer : IServicePlayer
    {
        private readonly string _connectionString;
        public ServicePlayer(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public Players AddPlayers(Players players)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);

            connection.Open();

            string query = "INSERT INTO Players(Name,Team) VALUES(@Name,@Team)";

            using SqlCommand command = new SqlCommand(query, connection);

           
            command.Parameters.AddWithValue("@Name",players.Name);
            command.Parameters.AddWithValue("@Team",players.Team);

            int rowAffected = command.ExecuteNonQuery();

            players.Id = rowAffected;

            return players;

        }
    }
}
