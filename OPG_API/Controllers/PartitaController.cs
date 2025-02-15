using Core;
using Core.Base;
using Microsoft.AspNetCore.Mvc;
using Primitives;
using Core.Game_dir;

namespace OPG_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartitaController : ControllerBase
    {
        private readonly ILogger<PartitaController> _logger;
        private readonly IServiceProvider _services;
        
        

        public PartitaController(ILogger<PartitaController> logger, IServiceProvider services)
        {
            _logger = logger;
            _services = services;

        }

        [HttpPost("StartGame")]

        public async Task<ActionResult> StartGame(string nome, int difficoltà, List<int> idPersonaggi)
        {
            var game = _services.GetService<Game>();

            game.NewGame(nome, difficoltà, idPersonaggi);

            return Ok();
        }

        [HttpPost("LoadGame")]

        public async Task<ActionResult> LoadGame(int idPartita)
        {
            var game = _services.GetService<Game>();

            game.LoadGame(idPartita);

            return Ok();
        }

        [HttpPost("SaveGame")]

        public async Task<ActionResult> SaveGame()
        {
            var game = _services.GetService<Game>();

            var messaggio = game.SalvaPartita();

            return Ok(messaggio);
        }
    }
}