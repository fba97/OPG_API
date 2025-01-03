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
        public async Task<string?> UpdateGameInformations()
        {
            var game = _services.GetService<Game>();

            return game?.ToString();
        }

        [HttpGet("GetUpdatePartitaAttuale")]
        public async Task<ActualPartita?> GetUpdatePartitaAttuale()
        {
            var game = _services.GetService<Game>();

            return game?.PartitaAttuale;
        }

        [HttpGet("Mappe")]
        // quando tiro su la mappa è sempre solo 1. fa niente per il momento.
        public IEnumerable<Mappa> Mappe()
        {
            var game = _services.GetService<Game>();

            if (game?.AllMappe is null)
                return new List<Mappa>();

            var ListaMappe = new List<Mappa> { game.AllMappe };
            return ListaMappe;
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