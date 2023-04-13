using Microsoft.AspNetCore.Mvc;

using Primitives;

namespace OPG_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonaggioController : ControllerBase
    {


        private readonly DataAccess _dataAccess;

        private readonly ILogger<PersonaggioController> _logger;

        public PersonaggioController(ILogger<PersonaggioController> logger, DataAccess da)
        {
            _logger = logger;
            _dataAccess = da;
        }

        [HttpGet(Name = "GetAllPersonaggi")]
        public async Task<IEnumerable<PersonaggioBase>> Get()
        {
            return await _dataAccess.GetAllCharacters();
        }
    }
}