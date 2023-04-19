using Microsoft.AspNetCore.Mvc;

using Primitives;

namespace OPG_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonaggioController : ControllerBase
    {


        private readonly DataAccess _dataAccess;

        private readonly ILogger<PersonaggioController> _logger;

        public PersonaggioController(ILogger<PersonaggioController> logger, DataAccess da)
        {
            _logger = logger;
            _dataAccess = da;
        }

        [HttpGet("GetAllPersonaggiBase")]
        
        public async Task<ActionResult<IEnumerable<PersonaggioBase>>> GetAllPersonaggiBase()
        {
            return Ok(await _dataAccess.GetAllPersonaggiBaseFromDb());
        }

        [HttpGet("GetAllPersonaggiInPartita")]
        public async Task<IEnumerable<PersonaggioInPartita>> GetAllPersonaggiInPartita()
        {
            return await _dataAccess.GetAllPersonaggiInPartitaFromDb();
        }

        [HttpGet("GetAllNpcs")]
        public async Task<IEnumerable<PersonaggioInPartita>> GetAllNpcs()
        {
            return await _dataAccess.GetAllNpcsFromDb();
        }

    }
}