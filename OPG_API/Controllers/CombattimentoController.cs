using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Primitives;

namespace OPG_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CombattimentoController : ControllerBase
    {


        private readonly DataAccess _dataAccess;

        private readonly ILogger<CombattimentoController> _logger;

        public CombattimentoController(ILogger<CombattimentoController> logger, DataAccess da)
        {
            _logger = logger;
            _dataAccess = da;
        }

        [HttpGet("GetCombattimentoById")]

        public async Task<ActionResult<Combattimento?>> GetAllCombattimentoById(int idCombattimento)
        {
            var resp = await _dataAccess.GetCombattimentoByIdFromDb(idCombattimento);
            if(resp is not null)
                return Ok(resp);
            else 
                return BadRequest("l'Id combattimento richiesto non è presente a database.");
        }

        [HttpGet("GetAllCombattimenti")]

        public async Task<ActionResult<IEnumerable<Combattimento>>> GetAllCombattimenti()
        {
            
            return Ok(await _dataAccess.GetAllCombattimentiFromDb());
        }


        [HttpPost("PostAttacco")]

        public async Task<ActionResult> PostAttacco(int idAttaccato, int idAttaccante)
        {
            var attaccato = await _dataAccess.GetPersonaggioInPartitaByIdFromDb(idAttaccato);
            var attaccante = await _dataAccess.GetPersonaggioInPartitaByIdFromDb(idAttaccante);
            attaccato.PuntiVitaPersonaggio -= attaccante.Attacco_InPartita;

            if(attaccato.PuntiVitaPersonaggio <0)
                attaccato.PuntiVitaPersonaggio = 0;

            var updateAttaccato = attaccato;

            var result = await _dataAccess.UpdatePersonaggioInPartitaByIdToDb(updateAttaccato);
            
            if (result==0)
                return BadRequest($"La chiamata al metodo PostAttacco non ha modificato nulla a database. I campi passati in input sono idAttaccato:{idAttaccato} e idAttaccante:{idAttaccante}");
            else
                return Ok();
        }



    }
}