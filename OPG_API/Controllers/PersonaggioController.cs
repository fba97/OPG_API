using Core.Base;
using Core.Game_dir;
using Microsoft.AspNetCore.Mvc;
using Primitives;

namespace OPG_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonaggioController : ControllerBase
    {
        private readonly ILogger<PersonaggioController> _logger;
        private readonly IServiceProvider _services;
        
        

        public PersonaggioController(ILogger<PersonaggioController> logger, IServiceProvider services)
        {
            _logger = logger;
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


        //[HttpGet("Getpersonaggio")]
        //public async Task<Core.Primitives.Personaggio?> Getpersonaggio()
        //{
        //    await using var uow = _services.GetUnitOfWork();
        //    var pg = await uow.PersonaggioRepository.GetByIdAsync(1);
        //    return pg;
        //}
    }
}