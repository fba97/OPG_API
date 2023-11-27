using System;
using System.Collections.Concurrent;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection.Metadata.Ecma335;
using Primitives;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Core.Game
{
    public partial class Game
    {

        private readonly IServiceProvider _services;
        private readonly IConfiguration _cfg;
        private readonly string _connString;
        private readonly ILogger<Game> _log;

        public InfoPartita? Partita { get => _partita; }
        private InfoPartita? _partita;
        public Mappa? Mappa { get => _mappa; }
        private Mappa? _mappa;

        public IEnumerable<Area> Aree { get => _aree; }
        private Area[] _aree;

        public IEnumerable<Tessera> Tessere { get => _tessere; }
        private Tessera[] _tessere;

        public IEnumerable<Punto> Punti { get => _punti; }
        private Punto[] _punti;

        public IEnumerable<Personaggio> Personaggi { get => _personaggi; }
        private Personaggio[] _personaggi;

        public IEnumerable<Oggetto> _Oggetti { get => _oggetti; }
        private Oggetto[] _oggetti;

        public IEnumerable<Passo> Passi { get => _passi; }
        private Passo[] _passi;

        // il processo di tirare su le partite potrebbe essere quresto:
        // quando viene avviato il programma non carico nulla.
        // quando il frontend mi da il comando LoadPartita(id) prendo le info relative ad una partita
        // se invece il comando è quello di una nuova partita 

        public Game
    (
        IConfiguration configuration,
        ILogger<Game> logger,
        IServiceProvider services
    )
        {
            _cfg = configuration;
            _log = logger;
            _services = services;
            _connString = _cfg.GetConnectionString("sqlStringConnection");
        }

        public async Task NewGameAsync(CancellationToken token = default)
        {
            try
            {
                CleanGameFromMemoryAsync();
                InitMap();
                InitPersonaggi();

                _partita = GetActualPartita();
                _log.LogInformation($"fine Bootstraping");
            }
            catch (Exception ex)
            {
                _log.LogCritical(ex.ToString());
                throw;
            }
        }

        private void CleanGameFromMemoryAsync()
        {
            this._partita = null;
            this._mappa = null;
            this._aree = new List<Area>().ToArray();
            this._tessere = new List<Tessera>().ToArray();
            this._punti = new List<Punto>().ToArray();
            this._personaggi = new List<Personaggio>().ToArray();
            this._oggetti = new List<Oggetto>().ToArray();
            this._passi = new List<Passo>().ToArray();
        }



        private void InitPersonaggi()
        {
            _log.LogInformation($"Inizio Caricamento personaggi e oggetti");

            _oggetti = GetAllOggetti().ToArray();
            _personaggi = GetAllPersonaggi().ToArray();

            //aggiungi ai personaggi stati e inventari
            //seleziona quali sono i personaggi realmente in gioco (li hai presi tutti per il momento)
            _log.LogInformation($"fine Caricamento personaggi e oggetti");
        }
        private void InitMap()
        {
            _log.LogInformation($"Initiating Game Map bootstrapping process");
            
            _punti = GetAllPunti().ToArray();
            _tessere = GetAllTessere().ToArray();
            _aree = GetAllAree().ToArray();
            _mappa = GetMappa(1);
            //_passi = GetPassi().ToArray();

            _log.LogInformation("Successful Game Map Bootstrap");

        }
    }
}
