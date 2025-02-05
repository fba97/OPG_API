using Core;
using Core.Base;
using Microsoft.AspNetCore.Mvc;
using Primitives;
using Core.Repository;
using Core.Game_dir;

namespace OPG_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UpdateController : ControllerBase
    {
        private readonly ILogger<UpdateController> _logger;
        private readonly IServiceProvider _services;
        
        

        public UpdateController(ILogger<UpdateController> logger, IServiceProvider services)
        {
            _logger = logger;
            _services = services;

        }

        [HttpGet("GetUpdateTotale")]
        public IActionResult UpdateGameInformations()
        {
            var game = _services.GetService<Game>();
            if (game == null)
                return NotFound();

            return Ok(game); 
        }

        [HttpGet("GetUpdatePartitaSoft")]
        public IActionResult GetUpdatePartitaAttuale()
        {
            var game = _services.GetService<Game>();
            if (game?.PartitaAttuale == null)
                return NotFound();

            var result = new ActualPartita()
            { 
                Id = game.PartitaAttuale.Id,
                Difficolta = game.PartitaAttuale.Difficolta,
                DataInizioPartita = game.PartitaAttuale.DataInizioPartita,
                DataFinePartita = game.PartitaAttuale.DataFinePartita,
                DataUltimoSalvataggio = game.PartitaAttuale.DataUltimoSalvataggio,
                IdObiettivo = game.PartitaAttuale.IdObiettivo,
                Inventari = game.PartitaAttuale.Inventari,
                IdGiocatore = game.PartitaAttuale.IdGiocatore,
                Nome = game.PartitaAttuale.Nome,
                Oggetti = game.PartitaAttuale.Oggetti,
                Personaggi = game.PartitaAttuale.Personaggi,
                StatoPartita= game.PartitaAttuale.StatoPartita,
                Combattimenti = game.PartitaAttuale.Combattimenti,
                Missioni = game.PartitaAttuale.Missioni,
                Punti = game.AllPunti,
                ActualTurno = game.PartitaAttuale.ActualTurno
            };

            return Ok(result);
        }

        [HttpGet("GetMappa")]
        public IActionResult GetMappa()
        {
            var game = _services.GetService<Game>();

            if (game is null || game.PartitaAttuale is null)
                return NotFound();

            var result = game.PartitaAttuale.Mappa;

            return Ok(result);
        }

        [HttpGet("Aree")]
        public IEnumerable<Area> Aree()
        {
            var game = _services.GetService<Game>();

            var listaAree = new List<Area>();

            return game?.AllAree ?? listaAree;
        }

        [HttpGet("Tessere")]
        public IEnumerable<Tessera> Tessere()
        { 
            var game = _services.GetService<Game>();

            var listaTessere = new List<Tessera>();
            
            return game?.AllTessere ?? listaTessere;
        }

        [HttpGet("Punti")]
        public IEnumerable<Punto> Punti()
        {
            var game = _services.GetService<Game>();

            var ListaPunti = new List<Punto>();

            return game?.AllPunti ?? ListaPunti;
        }

        [HttpGet("Personaggi")]
        public IEnumerable<Personaggio> Personaggi()
        {
            var game = _services.GetService<Game>();

            var ListaPersonaggi = new List<Personaggio>();

            return game?.AllPersonaggi ?? ListaPersonaggi;
        }

        [HttpGet("Oggetti")]
        public IEnumerable<Oggetto> Oggetti()
        {
            var game = _services.GetService<Game>();

            var ListaOggetti = new List<Oggetto>();

            return game?.AllOggetti ?? ListaOggetti;
        }

        [HttpGet("Adiacenze")]
        public IEnumerable<Adiacenza> Adiacenze()
        {
            var game = _services.GetService<Game>();

            var ListaAdiacenze = new List<Adiacenza>();

            return game?.AllAdiacenze ?? ListaAdiacenze;
        }
        [HttpGet("Inventari")]
        public IEnumerable<Inventario> Inventari()
        {
            var game = _services.GetService<Game>();

            var ListaInventari = new List<Inventario>();

            if (game == null || game.PartitaAttuale == null)
                return ListaInventari;

            return game.PartitaAttuale.Inventari ?? ListaInventari;
        }

        [HttpGet("Combattimenti")]
        public IEnumerable<Combattimento> Combattimenti()
        {
            var game = _services.GetService<Game>();

            var ListaCombattimenti = new List<Combattimento>();

            if (game == null || game.PartitaAttuale == null)
                return ListaCombattimenti;

            return game.PartitaAttuale.Combattimenti ?? ListaCombattimenti;
        }

        [HttpGet("Missioni")]
        public IEnumerable<Missione> Missioni()
        {
            var game = _services.GetService<Game>();

            var ListaMissioni = new List<Missione>();

            if (game == null || game.PartitaAttuale == null)
                return ListaMissioni;

            return game.PartitaAttuale.Missioni ?? ListaMissioni;
        }
    }
}