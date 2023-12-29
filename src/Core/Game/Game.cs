using System;
using System.Collections.Concurrent;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Reflection.Metadata.Ecma335;
using Primitives;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Core.Game
{
    public partial class Game
    {
        private readonly IConfiguration _cfg;
        private readonly string _connString;

        private readonly ILogger<Game> _log;

        public ActualPartita? PartitaAttuale { get => _partita; }
        private ActualPartita? _partita;

        public Mappa? AllMappe { get => _mappa; }
        private Mappa? _mappa;

        public IEnumerable<Area> AllAree { get => _aree; }
        private Area[] _aree;

        public IEnumerable<Tessera> AllTessere { get => _tessere; }
        private Tessera[] _tessere;

        public IEnumerable<Punto> AllPunti { get => _punti; }
        private Punto[] _punti;

        public IEnumerable<Personaggio> AllPersonaggi { get => _personaggi; }
        private Personaggio[] _personaggi;

        public IEnumerable<Oggetto> AllOggetti { get => _oggetti; }
        private Oggetto[] _oggetti;

        public IEnumerable<Adiacenza> AllAdiacenze { get => _adiacenze; }
        private Adiacenza[] _adiacenze;



        public Game
    (
        IConfiguration configuration,
        ILogger<Game> logger
    )
        {
            _cfg = configuration;
            _log = logger;
            _connString = _cfg.GetConnectionString("sqlStringConnection");
        }


        public bool NewGame(string nome, int difficoltà, int numeroGiocatori, int idObiettivo, int idGiocatore)
        {
            try
            {
                _log.LogInformation($"Inizio Bootstraping NewGame");
                CleanGameFromMemoryAsync();
                InitGeneralMapInfo();
                InitGeneralInfo();

                // in futuro quando avrò piu di una mappa dovrò dare la possibilità di scegliere tramite id_mappa
                InitNewActualInfo(nome, difficoltà, numeroGiocatori, idObiettivo, idGiocatore);


                _log.LogInformation($"fine Bootstraping NewGame");
            }
            catch (Exception ex)
            {
                _log.LogCritical(ex.ToString());
                throw;
            }
            return true;
        }
        public bool LoadGame(int idPartita)
        {
            try
            {
                _log.LogInformation($"Inizio Bootstraping LoadGame");
                CleanGameFromMemoryAsync();

                InitGeneralMapInfo();
                InitGeneralInfo();


                InitActualInfo(idPartita);


                _log.LogInformation($"fine Bootstraping LoadGame");
            }
            catch (Exception ex)
            {
                _log.LogCritical(ex.ToString());
                throw;
            }
            return true;
        }

        private void InitNewActualInfo(string nome, int difficoltà, int numeroGiocatori, int idObiettivo, int idGiocatore)
        {
            if (_partita is null)
                _partita = new ActualPartita
                    (0,
                    nome,
                    idGiocatore,
                    idObiettivo,
                    difficoltà,
                    1,
                    DateTime.Now,
                    null,
                    null,
                    string.Empty,
                    AllMappe,
                    AllAree,
                    AllTessere,
                    AllPunti,
                    AllPersonaggi,
                    AllOggetti,
                    AllAdiacenze,
                    new List<Inventario>(),
                    new List<Combattimento>(),
                    new List<Missione>());

        }
        private void InitActualInfo(int idPartita)
        {
            _partita = GetActualPartita(idPartita);
            if (_partita is not null && !string.IsNullOrEmpty(_partita.JSONSalvataggio))
            {
                // dentro jsonSalvataggio devono esserci dentro i persaonaggi, gli oggetti, la mappa, aree, tessere, punti, passi, combattimenti, missioni, inventari., stato generale della partita.
                var partitaJson = ActualPartita.Deserialize(_partita.JSONSalvataggio);

                _partita.Nome = partitaJson.Nome;
                _partita.IdGiocatore = partitaJson.IdGiocatore;
                _partita.IdObiettivo = partitaJson.IdObiettivo;
                _partita.Difficolta = partitaJson.Difficolta;
                _partita.StatoPartita = partitaJson.StatoPartita;
                _partita.DataInizioPartita = partitaJson.DataInizioPartita;
                _partita.DataUltimoSalvataggio = partitaJson.DataUltimoSalvataggio;
                _partita.DataFinePartita = partitaJson.DataFinePartita;
                _partita.JSONSalvataggio = partitaJson.JSONSalvataggio;
                _partita.Mappa = partitaJson.Mappa;
                _partita.Aree = partitaJson.Aree;
                _partita.Tessere = partitaJson.Tessere;
                _partita.Punti = partitaJson.Punti;
                _partita.Personaggi = partitaJson.Personaggi;
                _partita.Oggetti = partitaJson.Oggetti;
                _partita.Adiacenze = partitaJson.Adiacenze;
                _partita.Inventari = partitaJson.Inventari;
                _partita.Combattimenti = partitaJson.Combattimenti;
                _partita.Missioni = partitaJson.Missioni;
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
            this._adiacenze = new List<Adiacenza>().ToArray();
        }



        private void InitGeneralInfo()
        {
            _log.LogInformation($"Inizio Caricamento di tutti i personaggi e gli oggetti");

            _oggetti = GetAllOggetti().ToArray();
            _personaggi = GetAllPersonaggi().ToArray();

            _log.LogInformation($"fine Caricamento di tutti i personaggi e gli oggetti");
        }
        private void InitGeneralMapInfo()
        {
            _log.LogInformation($"Initiating Game Map bootstrapping process");

            _punti = GetAllPunti().ToArray();
            _tessere = GetAllTessere().ToArray();
            _aree = GetAllAree().ToArray();
            _mappa = GetMappa(1);
            _adiacenze = GetAllAdiacenze().ToArray();

            _log.LogInformation("Successful Game Map Bootstrap");

        }

        public string SalvaPartita()
        {
            if (_partita is null)
                return "Nessuna partita da salvare";

            _partita.JSONSalvataggio = _partita.Serialize();
            var id_Partita = SalvaPartitaToDB(_partita);
            _partita.Id = id_Partita;
            _partita.JSONSalvataggio = _partita.Serialize();
            _log.LogInformation($"È stata salvata la partita con id partita {id_Partita}");
            return $"È stata salvata la partita con id {id_Partita}";
        }


        public List<Personaggio> GetPersonaggiLocatedIn(Tessera tessera)
        {
            List<Punto> puntiDaControllare = (List<Punto>)tessera.Punti;

            var personaggiIncriminati = PartitaAttuale.Personaggi
                .Where(personaggio => puntiDaControllare.Any(punto => punto.Id == personaggio.Posizione))
                .ToList();

            return personaggiIncriminati;
        }

        public Punto GetPuntoById(int idPunto)
        => AllPunti.Where(p => p.Id == idPunto).First();



    }

}

