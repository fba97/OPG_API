using Core.Base;
using Core.Game_dir;
using Core.Map_Handling.Managers;
using Microsoft.AspNetCore.Mvc;
using Primitives;

namespace OPG_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovimentazioneController : ControllerBase
    {
        private readonly ILogger<MovimentazioneController> _logger;
        private readonly IServiceProvider _services;
        
        

        public MovimentazioneController(ILogger<MovimentazioneController> logger, IServiceProvider services)
        {
            _logger = logger;
            _services = services;
        }

        [HttpPost("creazione missione Test")]
        // pulisci le tabelle della partita attuale e riempile con la partita scelta tramite id
        public async Task<ActionResult> CreazioneMissione (int punto1, int punto2, int numeroPassi)
        {
            var _mm = _services.GetService<MissionManager>();

            var missione = _mm.CreateMission(punto1,punto2,1,numeroPassi);

            return Ok();
        }

    }
}