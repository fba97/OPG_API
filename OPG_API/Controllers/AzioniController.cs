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

        public AzioniController(ILogger<AzioniController> logger, IServiceProvider services)
        {
            _logger = logger;
            _services = services;
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
        public ActionResult Raccogli(int idPersonaggio, int idRaccolto, int tipologia) 
        {
            return Ok();
        }

        [HttpPost("Probabilità")]
        public ActionResult Probabilità(int idpersonaggio, int posizionePunto)
        {
            return Ok();
        }

        [HttpPost("Imprevisto")]
        public ActionResult Imprevisto(int idpersonaggio, int posizionePunto)
        {
            return Ok();
        }

        [HttpPost("ConcludiTurno")]
        public ActionResult ConcludiTurno(int idGiocatore)
        {
            return Ok();
        }

        [HttpPost("Attacco")]

        public async Task<ActionResult> Attacco(int idAttaccato, int idAttaccante, int valoreAttacco)
        {
            // è un nuovo combattimento?
            // se si crea un nuovo combattimento
            // se no e il pg è gia nel combattimento vai semplicemente a fare la sottrazione
            // se è un nuuovo giocatore che attacca un mostro gia in combattimento aggiungilo al combattimento

            var game = _services.GetService<Game>();

            if (game?.PartitaAttuale is null)
                return BadRequest("la partita non è stata caricata.");

            var attaccato = game.PartitaAttuale.Personaggi.FirstOrDefault(p => p.Id == idAttaccato);
            var attaccante = game.PartitaAttuale.Personaggi.FirstOrDefault(p => p.Id == idAttaccante);

            if(attaccante is null)
                return BadRequest("attaccante non trovato.");
            if (attaccato is null)
                return BadRequest("attaccato non trovato.");

            var combattimentiInteressati = game.PartitaAttuale.Combattimenti.Where(c => c.ListaEroi.Contains(attaccante.Id) 
                                                                                    || c.ListaEroi.Contains(attaccato.Id) 
                                                                                    || c.ListaNPCs.Contains(attaccante.Id) 
                                                                                    || c.ListaNPCs.Contains(attaccato.Id));

            // qua vabe sbatti perchè non ho amici/nemici ma ho 4 opzioni. da aggiungere valutazione su amici npc e nemici personaggi
            var nemico = attaccante.TipoPersonaggio == TipoPersonaggio.NemicoNPC ? attaccante : attaccato.TipoPersonaggio == TipoPersonaggio.NemicoNPC ? attaccato : null;
            var eroe = attaccante.TipoPersonaggio == TipoPersonaggio.AmicoPersonaggio ? attaccante : attaccato.TipoPersonaggio == TipoPersonaggio.AmicoPersonaggio ? attaccato : null;

            Combattimento combattimento;
            
            if (!combattimentiInteressati.Any())
            {
                var nomeCombattimento = string.Concat("Scontro con ", nemico is null ? "???" : nemico?.Nome);

                combattimento = new Combattimento()
                {
                    Id = 0,
                    Nome = nomeCombattimento,
                    ListaEroi = new List<int> () { eroe.Id },
                    ListaNPCs = new List<int>() { nemico.Id }
                };
            }
            else
            {
                combattimento = combattimentiInteressati.First();
                if(!combattimento.ListaEroi.Contains(eroe.Id))
                    combattimento.ListaEroi.Add(eroe.Id);
                if (!combattimento.ListaNPCs.Contains(nemico.Id))
                    combattimento.ListaNPCs.Add(nemico.Id);
            }

            //questa è un'operazione da fare con un semaforo ma per il momento va bene cosi easy
            //game.PartitaAttuale.Combattimenti.FirstOrDefault(c => c.Id == );
            



            //attaccato.PuntiVitaPersonaggio -= attaccante.Attacco_InPartita;

            //if (attaccato.PuntiVitaPersonaggio < 0)
            //    attaccato.PuntiVitaPersonaggio = 0;

            //var updateAttaccato = attaccato;

            //var result = await _dataAccess.UpdatePersonaggioInPartitaByIdToDb(updateAttaccato);

            //if (result == 0)
            //    return BadRequest($"La chiamata al metodo PostAttacco non ha modificato nulla a database. I campi passati in input sono idAttaccato:{idAttaccato} e idAttaccante:{idAttaccante}");
            //else
                return Ok();
        }

        [HttpPost("CreaEseguiMissione")]
        public async Task<ActionResult> CreaEseguiMissione(int idPersonaggio, int destinazione, int numeroPassi)
        {
            var _mm = _services.GetService<MissionManager>();
            if (_mm is null)
                return BadRequest("MissionManager non trovato");

            var game = _services.GetService<Game>();
            if (game is null)
                return BadRequest("Game non trovato");

            var partitaAttuale = game.PartitaAttuale;
            if (partitaAttuale is null)
                return BadRequest("Caricare una partita prima di eseguire missioni");

            var personaggio = partitaAttuale.Personaggi.FirstOrDefault(p => p.Id == idPersonaggio);
            if (personaggio is null)
                return BadRequest("Personaggio non trovato nella partita");

            var missione = _mm.CreateMission(personaggio, destinazione, numeroPassi);
            if (missione is null)
                return BadRequest();

            missione = _mm.ExecuteMission(missione);
            _mm.DeleteMission(missione.Id);

            if (missione.Stato == StatoMissione.Completata)
                return Ok();

            return BadRequest($"Esecuzione della missione in stato : {missione.Stato.ToString()}");
        }

    }
}