using Microsoft.AspNetCore.Mvc;

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
            var resp = await _dataAccess.GetAllCombattimentoByIdFromDb(idCombattimento);
            if(resp is not null)
                return Ok(resp);
            else 
                return BadRequest("l'Id combattimento richiesto non è presente a database.");
        }

        [HttpGet("GetAllCombattimenti")]

        public async Task<ActionResult<IEnumerable<Combattimento>>> GetAllCombattimenti()
        {
            //await _dataAccess.GetAllCombattimentiFromDb()
            return Ok(new List<Combattimento>());
        }


    }
}