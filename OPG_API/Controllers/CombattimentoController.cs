using Core.Game_dir;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Primitives;

namespace OPG_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CombattimentoController : ControllerBase
    {
        private readonly ILogger<CombattimentoController> _logger;
        private readonly IServiceProvider _services;

        public CombattimentoController(ILogger<CombattimentoController> logger, IServiceProvider services)
        {
            _logger = logger;
            _services = services;
        }


        //[HttpGet("GetAllCombattimenti")]

        //public async Task<ActionResult<IEnumerable<Combattimento>>> GetAllCombattimenti()
        //{
        // //gli devo passare l'oggetto game dal quale prendo la lista dei combattimenti   
        //    return Ok();
        //}


        [HttpPost("Attacco")]

        public async Task<ActionResult> Attacco(int idAttaccato, int idAttaccante, int valoreAttacco)
        {
            // è un nuovo combattimento?
            // se si crea un nuovo combattimento
            // se no e il pg è gia nel combattimento vai semplicemente a fare la sottrazione
            // se è un nuuovo giocatore che attacca un mostro gia in combattimento aggiungilo al combattimento

            var game = _services.GetService<Game>();

            var attaccato = await _dataAccess.GetPersonaggioInPartitaByIdFromDb(idAttaccato);
            var attaccante = await _dataAccess.GetPersonaggioInPartitaByIdFromDb(idAttaccante);
            attaccato.PuntiVitaPersonaggio -= attaccante.Attacco_InPartita;

            if (attaccato.PuntiVitaPersonaggio < 0)
                attaccato.PuntiVitaPersonaggio = 0;

            var updateAttaccato = attaccato;

            var result = await _dataAccess.UpdatePersonaggioInPartitaByIdToDb(updateAttaccato);

            if (result == 0)
                return BadRequest($"La chiamata al metodo PostAttacco non ha modificato nulla a database. I campi passati in input sono idAttaccato:{idAttaccato} e idAttaccante:{idAttaccante}");
            else
                return Ok();
        }

    }
}