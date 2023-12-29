using Core.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Primitives;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Core;
using Core.Game;

namespace Core.Map_Handling.Managers
{
    public class MissionManager
    {
        private readonly string _connString;
        private readonly Game.Game _game;
        private readonly List<Missione> _missions;

        private readonly ILogger<MissionManager> _log;
        private readonly IConfiguration _cfg;
        private readonly IServiceProvider _services;

        public MissionManager
           (
                ILogger<MissionManager> logger,
               IConfiguration configuration,
               IServiceProvider serviceProvider,
               Core.Game.Game game               
           )
        {
            _log = logger;
            _connString = configuration.GetConnectionString("ConnectionString");
            _services = serviceProvider;
            _game = game;
        }
        private record DirezioneAdiacenza(int IdPuntoUno, int IdPuntoDue);


        public Missione? CreateMission(int idActualPunto, int idDestinationPunto, int idPersonaggio, int numeroPassi)
        {
            //controllo se effettivamente puo muoversi o cosa ( controllo se nella casella ci sono o no nemici. se si riturno null)
            // per fare questa cosa devo prima capire in che esagono mi trovo. una volta che ho capito in che esagono sono devo andare a vedere quali altri punti sono nello stesso esagono
            //il top sarebbe avere un metodo che mi permetta di avere una lista di punti appartenenti a quell'esagono (perchè sicuro mi servirà ancora)
            if (NemiciNellaTessera(idActualPunto))
                return null;

            var shortestPath = GetShortestPath(idActualPunto, idDestinationPunto);

            if (shortestPath == null || !shortestPath.Any())
                return null;

            var adiacenzeMissione = shortestPath.Take(numeroPassi);


            int newMissionId = _missions.Any() ? _missions.Last().Id + 1 : 1;
            var passi = CreaPassiDaAdiacenze(adiacenzeMissione, idActualPunto);
            // creo la missione generando tutti i primi N passi
            var missione = new Missione
            {
                Id = newMissionId,   
                TipoMissione = TipoMissione.Spostamento,
                Partenza = idActualPunto,
                Destinazione = idDestinationPunto,
                Stato = StatoMissione.Nuova,
                Passi = passi
            };

            return missione;
        }

        private List<Passo> CreaPassiDaAdiacenze(IEnumerable<Adiacenza> adiacenzeMissione, int idActualPunto)
        {
            List<Passo> percorsoPassi = new();

            for(int i = 0; i<adiacenzeMissione.Count(); i++)
            {
                var direzioneAdiacenza = OrdinaDirezioneSingoloPasso(adiacenzeMissione,i, idActualPunto);

                percorsoPassi.Add(new Passo(i,
                                            direzioneAdiacenza.IdPuntoUno,
                                            direzioneAdiacenza.IdPuntoDue,
                                            null,
                                            StatoPasso.Nuovo
                                            ));
            }

            return percorsoPassi;
        }

        private DirezioneAdiacenza OrdinaDirezioneSingoloPasso(IEnumerable<Adiacenza> adiacenzeMissione, int i, int idActualPunto)
        {

            var passoCorrente = adiacenzeMissione.ElementAt(i);

            if (i == 0)
            {
                // Ordina la direzione in modo che vada da idActualPunto al secondo punto dell'adiacenza
                if (passoCorrente.IdPuntoUno == idActualPunto)
                    return new DirezioneAdiacenza(passoCorrente.IdPuntoUno, passoCorrente.IdPuntoDue);
                else
                    return new DirezioneAdiacenza(passoCorrente.IdPuntoDue, passoCorrente.IdPuntoUno);                
            }

            var passoPrecedente = adiacenzeMissione.ElementAt(i - 1);
        
            //qui devo controllare quale dei punti presenti in questa adiacenza è presente anche nella precedente
            //se un punto dell'adiacenza i è presente anche nell'adiacenza i-1 so che devo metterlo come IdpuntoUno altrimenti come idPuntoDue
            if (passoCorrente.IdPuntoUno == passoPrecedente.IdPuntoUno || passoCorrente.IdPuntoUno == passoPrecedente.IdPuntoDue)
                return new DirezioneAdiacenza(passoCorrente.IdPuntoUno, passoCorrente.IdPuntoDue);
            else
                return new DirezioneAdiacenza(passoCorrente.IdPuntoDue, passoCorrente.IdPuntoUno);
            
            
        }
    
        public Missione ExecuteMission(Missione missione)
        {
            //ciclo tutti i passi della missione e vedo se effettivamente sono eseguibili
            //se non lo sono ritorno come stato missione Interrotta

            foreach (var passo in missione.Passi)
            {
                var statoPasso = ExecuteStep(passo);

                if (statoPasso == StatoPasso.Errore)
                {
                    missione.Stato = StatoMissione.Interrotta;
                    break;
                }
            }

            return missione;

            //update della posizione del personaggio
        }

        public StatoPasso ExecuteStep(Passo passo)
        {
            // controlla che non ci siano nemici nella casella dove stai andando (prendendo da game tutti i personaggi e scorrendoli controllando gli IdPassi e il tipo personaggio per capire se sono nemici
            if (NemiciNellaTessera(passo.Id))
                return StatoPasso.Errore;

            // Aggiorna la posizione del personaggio
            // ...

            return StatoPasso.Completato;
        }

        public Missione DeleteMission(int idMissione)
        {
            var missione = _missions.FirstOrDefault(m => m.Id == idMissione);

            if (missione != null)
                _missions.Remove(missione);

            return missione;
        }

            private IEnumerable<Adiacenza> GetShortestPath(int idActualPunto, int idDestinationPunto) 
        {

            //ragionandola io mi verrebbe da dire che
            //tiro su tutte le adiacenze della mappa (sono tutte bidirezionali)
            var adiacenze = _game.AllAdiacenze;

            //all'interno di un while
            //prendo tutte quelle adiacenze che iniziano con idActualPunto come sorgente o destinazione tanto sono bidirezionali
            //trovo N adiacenze. se le adiacenze sono relative a punti gia presenti nel path le escludo. se no vado a creare N nuovi path uguali ai quali vado ad aggiungere la nuova adiacenza.
            //Ogni ciclo vado ad aggiungere un'adiacenza ad ogni path.
            //Il primo path che trova l'adiacenza con destinazione uguale all'idDestinazionePunto viene ritornato in quandto piu corto.
            List<Adiacenza> initialPath = new List<Adiacenza>();
            List<List<Adiacenza>> paths = new List<List<Adiacenza>>
            {
                initialPath
            };

            var idPuntoPerNuovoNodo = idActualPunto;

            foreach (var path in paths) 
            {
                if (path.Count == 1)
                    if (path.First().IdPuntoUno == idActualPunto)
                        idPuntoPerNuovoNodo = path.First().IdPuntoDue;
                    else
                        idPuntoPerNuovoNodo = path.First().IdPuntoUno;
                else
                {
                    var ultimoNodo = path.Last();
                    idPuntoPerNuovoNodo = ultimoNodo.IdPuntoUno == idPuntoPerNuovoNodo
                        ? ultimoNodo.IdPuntoDue
                        : ultimoNodo.IdPuntoUno;
                }

                List<Adiacenza> PossibiliNuoveAdiacenze = adiacenze.Where(a => (a.IdPuntoUno == idPuntoPerNuovoNodo 
                                                                 || a.IdPuntoDue == idPuntoPerNuovoNodo)
                                                                    && a.Abilitata == true).ToList();
          

                if (!PossibiliNuoveAdiacenze.Any())
                    continue;

                //qui voglio fare un confronto tra le due liste di adiacenze. tra quelle che potrei aggiungere e quelle gia presenti nel path.
                //se una di quelle che voglio aggiungere è gia in quelle del path la escludo da quelle da aggiungere.

                // rimuovo tutte quelle adiacenze che erano gia presenti nel path
                PossibiliNuoveAdiacenze.RemoveAll(pna => path.Any(a => a.Id == pna.Id));
                // ora devo rimuovere tutte quele adiacenze che ritornano su un punto gia percorso nel path.
                PossibiliNuoveAdiacenze.RemoveAll(pna =>
                                                    (path.Any(a =>      a.IdPuntoUno == pna.IdPuntoUno 
                                                                    ||  a.IdPuntoDue == pna.IdPuntoUno) 
                                                                    && idActualPunto != pna.IdPuntoUno) 
                                                                    || 
                                                     (path.Any(a =>     a.IdPuntoUno == pna.IdPuntoDue 
                                                                    ||  a.IdPuntoDue == pna.IdPuntoDue) 
                                                                    && idActualPunto != pna.IdPuntoDue)
                                                     );

                if (PossibiliNuoveAdiacenze.Count > 1)
                    //crea N nuovi rami in un ciclo for
                    foreach (var adiacenza in PossibiliNuoveAdiacenze)
                    {
                        // qui devo controllare che i path vengano inseriti e calcolati nella successiva iterazione
                        var newPath = path;
                        newPath.Add(adiacenza); 
                        paths.Add(newPath);
                    }
                //if(PossibiliNuoveAdiacenze.Count == 0)
                    //se non sono arrivato al mio punto niente. cancello questo percorso o non lo calcolo alla fine
                if(PossibiliNuoveAdiacenze.Count == 1)
                    //aggiungi l'adiacenza al path attuale
                    path.Add(PossibiliNuoveAdiacenze[0]);

                if(path.Last().IdPuntoUno == idDestinationPunto || path.Last().IdPuntoDue == idDestinationPunto)
                {
                    return path; 
                }
            }
            return new List<Adiacenza>();
        }

        private bool NemiciNellaTessera(int idActualPunto)
        {
            Punto punto = _game.GetPuntoById(idActualPunto);
            var tessera = punto.LocatedIn;

            var personaggiNellaTessera = _game.GetPersonaggiLocatedIn(tessera);

            var ciSonoNemiciNellaTessera = personaggiNellaTessera.Count(p => p.TipoPersonaggio == TipoPersonaggio.NemicoNPC
                                                                          || p.TipoPersonaggio == TipoPersonaggio.NemicoPersonaggio);

            if (ciSonoNemiciNellaTessera > 0)
                return true;
            return false;
        }
    }
    

}
