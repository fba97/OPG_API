using Core;
using Core.Base;
using Microsoft.AspNetCore.Mvc;
using Primitives;
using Core.Repository;
using Core.Primitives;

namespace OPG_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartitaController : ControllerBase
    {
        private readonly ILogger<PersonaggioController> _logger;
        private readonly IServiceProvider _services;
        
        

        public PartitaController(ILogger<PersonaggioController> logger, IServiceProvider services)
        {
            _logger = logger;
            _services = services;

        }

        [HttpGet("GetPartitaById")]
        public async Task<Core.Primitives.Partita?> GetPartitaById()
        {
            await using var uow = _services.GetUnitOfWork();
            var partita = await uow.PartitaRepository.GetByIdAsync(9);
            return partita;
        }


        [HttpGet("GetAllPartite")]
        public async Task<IEnumerable<Core.Primitives.Partita>> GetAllPartite()
        {
            await using var uow = _services.GetUnitOfWork();
            var partite = await uow.PartitaRepository.GetAllAsync();
            return partite;
        }


        [HttpPost("CreaPartita")]

        public async Task<ActionResult> CreaPartita(int id_Giocatore, string nomePartita)
        {

            await using var uow = _services.GetUnitOfWork();
            var partita = new Core.Primitives.Partita(0, id_Giocatore, nomePartita, idObiettivo: 0, difficolta: 0, statoPartita: 1, dataInizioPartita: DateTime.Now, dataFinePartita: null, dataUltimoSalvataggio: null, oggettiInGioco: null, personaggiInGioco: null);
            var partitaAggiunta = await uow.PartitaRepository.AddAsync(partita);

            await uow.SaveChangesAsync();

            if (partita is null)
                return BadRequest($"La Partita non è stata creata.");
            else
                return Ok();
        }


        [HttpPost("LoadPartitaById")]
        // pulisci le tabelle della partita attuale e riempile con la partita scelta tramite id
        public async Task<ActionResult> LoadPartita(int idPartita)
        {
            await using var uow = _services.GetUnitOfWork();
            var partitaAggiunta = await uow.PartitaRepository.DeleteAsync(idPartita);

            await uow.SaveChangesAsync();

            return Ok();
        }


        [HttpPost("CancellaPartitaById")]

        public async Task<ActionResult> CancellaPartita(int idPartita)
        {
            await using var uow = _services.GetUnitOfWork();
            var partitaAggiunta = await uow.PartitaRepository.DeleteAsync(idPartita);

            await uow.SaveChangesAsync();
            
            return Ok();
        }

    }
}