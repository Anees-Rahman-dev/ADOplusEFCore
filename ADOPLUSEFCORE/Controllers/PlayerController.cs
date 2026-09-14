using ADOPLUSEFCORE.Data;
using ADOPLUSEFCORE.models;
using ADOPLUSEFCORE.Services;
using Microsoft.AspNetCore.Mvc;

namespace ADOPLUSEFCORE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly IServicePlayer _Service;

        public PlayerController(IServicePlayer service)
        {
            _Service = service;
        }
        private readonly AppDbContext _context;
        public PlayerController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public Players AddPlayer(Players players)
        {
            return _Service.AddPlayers(players);
        }
        [HttpPost("EF")]

        public async Task<IActionResult> AddPLAyers()
        {
            
        }
        public Task<IActionResult> GetPlayers()
        {

        }
    }
}
