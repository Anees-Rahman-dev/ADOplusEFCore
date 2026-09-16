using ADOPLUSEFCORE.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

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

        public List<Players> GetPlayers()
        {
            List<Players> players = new List<Players>();
            using SqlConnection connection = new SqlConnection(_connectionString);

            connection.Open();

            string query = "Select Id,Name,Team FROM Players";

            using SqlCommand command = new(query,connection);

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                Players player = new Players{

                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    Team = reader["Team"].ToString()
                };
                players.Add(player);
            }
                return players;
        }

        public void DeletePlayer(int id)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string Q = "DELETE FROM Players WHERE Id = @id";

            using SqlCommand cmd = new SqlCommand(Q, connection);

            cmd.Parameters.AddWithValue("@id",id);

            cmd.ExecuteNonQuery();

        }

        public Players UpdatePlayer(int id, Players player)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);

            connection.Open();

            string Q = "UPDATE Players SET Name = @Name, Team = @Team WHERE Id = @Id";

            using SqlCommand command = new SqlCommand(Q, connection);

            command.Parameters.AddWithValue("@Id",id);
            command.Parameters.AddWithValue("@Name",player.Name);
            command.Parameters.AddWithValue("@Team",player.Team);

            int rowAffected = command.ExecuteNonQuery();

            return player;

        }

        public Players? GetPlayerById(int id)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);

            connection.Open();

            string Q = "SELECT Id, Name, Team FROM Players WHERE Id = @id";

            using SqlCommand comm = new SqlCommand(Q,connection);

            comm.Parameters.AddWithValue("@id",id);

            using SqlDataReader reader = comm.ExecuteReader();

            if (reader.Read())
            {
                Players players = new Players
                {

                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString()!,
                    Team = reader["Team"].ToString()!
                };
                return players;

            }
            return null!;

        }


    }
}
