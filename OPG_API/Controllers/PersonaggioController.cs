using Core.Base;
using Core.Game;
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
        private readonly IServiceProvider _services;
        
        

        public PersonaggioController(ILogger<PersonaggioController> logger, DataAccess da, IServiceProvider services)
        {
            _logger = logger;
            _dataAccess = da;
            _services = services;

        }

        //[HttpGet("GetAllPersonaggiBase")]
        
        //public async Task<ActionResult<IEnumerable<PersonaggioBase>>> GetAllPersonaggiBase()
        //{
        //    return Ok(await _dataAccess.GetAllPersonaggiBaseFromDb());
        //}

        //[HttpGet("GetAllPersonaggiInPartita")]
        //public async Task<IEnumerable<PersonaggioInPartita>> GetAllPersonaggiInPartita()
        //{
        //    return await _dataAccess.GetAllPersonaggiInPartitaFromDb();
        //}

        //[HttpGet("GetAllNpcs")]
        //public async Task<IEnumerable<PersonaggioInPartita>> GetAllNpcs()
        //{
        //    return await _dataAccess.GetAllNpcsFromDb();
        //}

        [HttpGet("GetCount")]
        public async Task<int> GetCount()
        {
            var conto = Game.Counto;
            return conto;
        }

        [HttpGet("Getpersonaggio")]
        public async Task<Core.Primitives.PersonaggioInPartita?> Getpersonaggio()
        {
            await using var uow = _services.GetUnitOfWork();
            var pg = await uow.PersonaggioRepository.GetByIdAsync(1);
            return pg;
        }
    }
}