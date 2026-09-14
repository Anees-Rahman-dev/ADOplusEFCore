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

        [HttpPost]
        public Players AddPlayer(Players players)
        {
            return _Service.AddPlayers(players);
        }
    }
}
