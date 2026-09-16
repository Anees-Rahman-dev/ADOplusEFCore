using ADOPLUSEFCORE.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Data;
using System.Runtime.Intrinsics.X86;

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

        public DataSet GetAllPlayersAndSave()
        {


            DataSet SET = new DataSet();

            using SqlConnection connection = new SqlConnection(_connectionString);

            string Q = "SELECT Id, Name, Team FROM Players";

            using SqlDataAdapter adapter = new SqlDataAdapter(Q,connection);

            adapter.Fill(SET,"Players");

            DataTable table = SET.Tables["Players"]!;

            return SET;

        }

        public DataTable GetPlayersWhoseTeam()
        {
            DataSet Set = new DataSet();

            using SqlConnection connection = new SqlConnection(_connectionString);

            string Q = "SELECT Id, Name, Team FROM Players";

            using SqlDataAdapter adapter = new SqlDataAdapter(Q,connection);

            adapter.Fill(Set, "Players");

            DataTable table = Set.Tables["Players"]!;

            DataView view = new DataView(table);

            view.RowFilter = "Team = 'Ac Milan'";

            return view.ToTable();

        }

        public DataSet InsertingPlayers(Players players)
        {
            DataSet Set = new DataSet();

            using SqlConnection connection = new SqlConnection(_connectionString);

            string Q = "SELECT Id, Name, Team FROM Players";

            using SqlDataAdapter adapter = new SqlDataAdapter(Q,connection);

            adapter.Fill(Set,"Players");

            adapter.InsertCommand = new SqlCommand("INSERT INTO Players (Name,Team) VALUES(@Name, @Team)",connection);

            adapter.InsertCommand.Parameters.Add("@Name",SqlDbType.VarChar,100,"Name");
            adapter.InsertCommand.Parameters.Add("@Team",SqlDbType.VarChar,100,"Team");

            DataTable table = Set.Tables["Players"]!;

            DataRow row = table.NewRow();

            row["Name"] = players.Name;
            row["Team"] = players.Team;

            table.Rows.Add(row);
            
            adapter.Update(Set,"Players");

            Set.Clear();

            adapter.Fill(Set, "Players");

            

            return Set;
        }
    }
}
