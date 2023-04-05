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

        public PersonaggioController(ILogger<PersonaggioController> logger)
        {
            _logger = logger;
            _dataAccess = new DataAccess("Server=192.168.1.74;Database=OPG_DB;Trusted_Connection=True;TrustServerCertificate=true;");
        }

        [HttpGet(Name = "GetAllPersonaggi")]
        public async Task<IEnumerable<PersonaggioBase>> Get()
        {
            return await _dataAccess.GetAllCharacters();
        }
    }
}