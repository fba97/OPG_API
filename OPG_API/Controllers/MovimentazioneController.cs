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

        [HttpPost("CreateAndExecuteMission")]
        public async Task<ActionResult> CreateAndExecuteMission(int punto1, int punto2, int idPersonaggio, int numeroPassi)
        {
            var _mm = _services.GetService<MissionManager>();
      
            if (_mm is null)
                return BadRequest();

            var missione = _mm.CreateMission(punto1,punto2, idPersonaggio, numeroPassi);
            if (missione is null)
                return BadRequest();

            missione = _mm.ExecuteMission(missione);
            _mm.DeleteMission(missione.Id);

            if (missione.Stato == StatoMissione.Completata)
                return Ok();

            return BadRequest($"Esecuzione della missione in stato : {missione.Stato.ToString()}");
        }

    }
}