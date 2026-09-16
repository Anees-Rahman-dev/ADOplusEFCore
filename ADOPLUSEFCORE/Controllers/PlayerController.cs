using ADOPLUSEFCORE.Data;
using ADOPLUSEFCORE.models;
using ADOPLUSEFCORE.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace ADOPLUSEFCORE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly IServicePlayer _Service;
        private readonly AppDbContext _context;

        public PlayerController(IServicePlayer service, AppDbContext context)
        {
            _Service = service;
            _context = context;
        }


        [HttpPost]
        public Players AddPlayer(Players players)
        {
            return _Service.AddPlayers(players);
        }
        [HttpPost("EF")]

        public async Task<IActionResult> AddPLAyers(Players players)
        {
            _context.Players.Add(players);

            await _context.SaveChangesAsync();

            return Ok(players);
        }


        [HttpGet]
        public List<Players> GetPlayers()
        {
            return _Service.GetPlayers();

        }

        [HttpGet("EF")]
        public async Task<IActionResult> GetAllPlayers()
        {
            var Playerss = await _context.Players.ToListAsync();

            return Ok(Playerss);
        }

        [HttpDelete]
        public void DeletePlayer(int id)
        {
            _Service.DeletePlayer(id);
        }

        [HttpDelete("{id}/EF")]
        public async Task<IActionResult> DeletePlayerr(int id)
        {
            var player = await _context.Players.FindAsync(id);

            if (player == null)
            {
                return NotFound();
            }
            else
            {
                _context.Players.Remove(player);
            }
            await _context.SaveChangesAsync();
            return Ok("Student Has Deleted");
        }

        [HttpPut]
        public Players UpdatePlayer(int id, Players players)
        {
            return _Service.UpdatePlayer(id, players);
        }

        [HttpPut("EF")]
        public async Task<IActionResult> UpdatePLayer(int Id, Players players)
        {
            var player = await _context.Players.FindAsync(Id);

            if (player == null)
            {
                return NotFound();
            }
            else
            {
                player.Name = players.Name;
                player.Team = players.Team;

            }
            await _context.SaveChangesAsync();
            return Ok(players);
        }

        [HttpGet("{id}")]
        public Players? GetPlayerById(int id)
        {
            return _Service.GetPlayerById(id);
        }

        [HttpGet("{id}/EF")]

        public async Task<IActionResult> GetplayerById(int id)
        {
            var player = await _context.Players.FindAsync(id);

            if (player == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(player);
            }
        }

        [HttpGet("DatasetGet")]
        public IActionResult GetAllPlayersAndSave()
        {
            DataSet Set = _Service.GetAllPlayersAndSave();

            DataTable table = Set.Tables["Players"]!;

            List<Players> playersList = new List<Players>();

            foreach(DataRow row in table.Rows)
            {
                playersList.Add(new Players
                {

                    Id = Convert.ToInt32(row["Id"]),
                    Name = row["Name"].ToString()!,
                    Team = row["Team"].ToString()!
                });

            }
            return Ok(playersList);

        }
    }
}
