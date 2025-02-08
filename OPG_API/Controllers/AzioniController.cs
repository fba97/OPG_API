using Core.Game_dir;
using Core.Map_Handling.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Primitives;
using System.Runtime.CompilerServices;

namespace OPG_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AzioniController : ControllerBase
    {
        private readonly ILogger<AzioniController> _logger;
        private readonly IServiceProvider _services;
        private readonly ActionManager _actionManager;

        public AzioniController(ILogger<AzioniController> logger, IServiceProvider services)
        {
            _logger = logger;
            _services = services;
            _actionManager = services.GetRequiredService<ActionManager>();  
        }



        [HttpPost("Effetto")]

        public ActionResult Effetto(int idattuatore, IEnumerable<int> idSoggetti)
        {
            return Ok();
        }

        [HttpPost("Scambia")]
        public ActionResult Scambia(int idSorgente, int idADestinazione, int idOggetto)
        {
            return Ok();
        }

        [HttpPost("Raccogli")]
        // tipologia in base alla tipologia di oggetto? sviluppi possibili
        public ActionResult Raccogli() 
        {
            var game = _services.GetService<Game>();
            if (game?.PartitaAttuale == null)
                return BadRequest("Caricare una partita prima di eseguire missioni");

            var idPersonaggio = game.PartitaAttuale.ActualTurno.IdDelPersonaggioInTurno;

            var personaggio = game.PartitaAttuale.Personaggi.FirstOrDefault(p => p.Id == idPersonaggio);
            if (personaggio is null)
                return BadRequest("Personaggio non trovato nella partita");

            var oggetto = game.PartitaAttuale.Inventari.Where(i => i.Tipo == TipoInventario.Mappa)
                                                       .SelectMany(i => i.Oggetti)
                                                       .FirstOrDefault(o => o.Oggetto.Id_Posizione == personaggio.Posizione);
            if (oggetto is null)
                return BadRequest("Oggetto da raccogliere non trovato nella partita");

            var spostaOggetto = new SpostaOggetto(personaggio, oggetto.Oggetto, TipoAzione.Raccogli);

            var result = _actionManager.ExecuteAction(spostaOggetto);

            return result;
        }

        [HttpPost("Probabilità")]
        public ActionResult Probabilità()
        {
            return Ok();
        }

        [HttpPost("Imprevisto")]
        public ActionResult Imprevisto()
        {
            return Ok();
        }

        [HttpPost("ConcludiTurno")]
        public ActionResult ConcludiTurno()
        {
            var game = _services.GetService<Game>();
            var actionManager = _services.GetService<ActionManager>();

            if (game is null)
                return BadRequest("Game is null");

            var partitaAttuale = game.PartitaAttuale;
            if (partitaAttuale is null)
                return BadRequest("Caricare una partita prima di cambiare turno.");

            actionManager.EndTurn(partitaAttuale.ActualTurno);


            return Ok();
        }
    
        [HttpPost("Attacco")]

        public async Task<ActionResult> Attacco(int idAttaccato, int idAttaccante)
        {
            var game = _services.GetService<Game>();
            if (game?.PartitaAttuale == null)
                return BadRequest("Caricare una partita prima di eseguire missioni");

            var attaccante = game.PartitaAttuale.Personaggi.FirstOrDefault(p => p.Id == idAttaccante);
            if (attaccante is null)
                return BadRequest("Personaggio Attaccante non trovato nella partita");

            var Attaccato = game.PartitaAttuale.Personaggi.FirstOrDefault(p => p.Id == idAttaccato);
            if (Attaccato is null)
                return BadRequest("Personaggio Attaccante non trovato nella partita");

            var attacco = new Attacco(Attaccato, attaccante);

            var result = _actionManager.ExecuteAction(attacco);

            return result;
        }

        [HttpPost("CreaEseguiMissione")]
        public async Task<ActionResult> CreaEseguiMissione(int destinazione)
        {
            var game = _services.GetService<Game>();
            if (game?.PartitaAttuale == null)
                return BadRequest("Caricare una partita prima di eseguire missioni");

            var idPersonaggio = game.PartitaAttuale.ActualTurno.IdDelPersonaggioInTurno;

            var personaggio = game.PartitaAttuale.Personaggi.FirstOrDefault(p => p.Id == idPersonaggio);
            if (personaggio == null)
                return BadRequest("Personaggio non trovato nella partita");

            var missione = new Missione(personaggio, destinazione);
            
            var result = _actionManager.ExecuteAction(missione);

            return result;
        }
    }
}