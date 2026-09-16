using ADOPLUSEFCORE.models;
using System.Data;

namespace ADOPLUSEFCORE.Services
{
    public interface IServicePlayer
    {
        Players AddPlayers(Players players);

        List<Players> GetPlayers();

        void DeletePlayer(int id);

        Players UpdatePlayer(int id,Players player);

        Players? GetPlayerById(int id);
        DataSet GetAllPlayersAndSave();
        DataTable GetPlayersWhoseTeam();
        DataSet InsertingPlayers(Players players);


        
    }
}
