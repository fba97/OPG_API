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
using Core.Game_dir;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Map_Handling.Managers
{
    public class MissionManager
    {
        private readonly string _connString;
        private Game _game;
        private readonly List<Missione> _missions;
        private const int MAX_PATH_LENGTH = 100;
        private const int MAX_PATHS = 1000;

        private readonly ILogger<MissionManager> _log;
        private readonly IConfiguration _cfg;
        private readonly IServiceProvider _services;

        public MissionManager
           (
                ILogger<MissionManager> logger,
               IConfiguration configuration,
               IServiceProvider serviceProvider,
               Game game
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
            if (NemiciNellaTessera(idActualPunto))
                return null;

            var shortestPath = GetShortestPath(idActualPunto, idDestinationPunto);

            if (shortestPath == null || !shortestPath.Any())
                return null;

            var adiacenzeMissione = shortestPath.Take(numeroPassi);

            int newMissionId = (_missions != null && _missions.Any()) ? _missions.Last().Id + 1 : 1;
            var passi = CreaPassiDaAdiacenze(adiacenzeMissione, idActualPunto);

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

            for (int i = 0; i < adiacenzeMissione.Count(); i++)
            {
                var direzioneAdiacenza = OrdinaDirezioneSingoloPasso(adiacenzeMissione, i, idActualPunto);

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
                if (passoCorrente.IdPuntoUno == idActualPunto)
                    return new DirezioneAdiacenza(passoCorrente.IdPuntoUno, passoCorrente.IdPuntoDue);
                else
                    return new DirezioneAdiacenza(passoCorrente.IdPuntoDue, passoCorrente.IdPuntoUno);
            }

            var passoPrecedente = adiacenzeMissione.ElementAt(i - 1);

            if (passoCorrente.IdPuntoUno == passoPrecedente.IdPuntoUno || passoCorrente.IdPuntoUno == passoPrecedente.IdPuntoDue)
                return new DirezioneAdiacenza(passoCorrente.IdPuntoUno, passoCorrente.IdPuntoDue);
            else
                return new DirezioneAdiacenza(passoCorrente.IdPuntoDue, passoCorrente.IdPuntoUno);
        }

        public Missione ExecuteMission(Missione missione)
        {
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
        }

        public StatoPasso ExecuteStep(Passo passo)
        {
            if (NemiciNellaTessera(passo.Id))
                return StatoPasso.Errore;

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
            var adiacenze = _game.AllAdiacenze;
            List<List<Adiacenza>> paths = new List<List<Adiacenza>>();
            List<Adiacenza> shortestPath = null;
            int minLength = int.MaxValue;

            // Initialize with first adjacent nodes
            var initialNodes = adiacenze.Where(a => (a.IdPuntoUno == idActualPunto || a.IdPuntoDue == idActualPunto) && a.Abilitata).ToList();
            foreach (var node in initialNodes)
            {
                paths.Add(new List<Adiacenza> { node });
            }

            while (paths.Any() && paths.Count < MAX_PATHS)
            {
                var currentPathsCount = paths.Count;
                for (int i = 0; i < currentPathsCount; i++)
                {
                    if (i >= paths.Count) break;

                    var currentPath = paths[i];
                    if (currentPath.Count >= MAX_PATH_LENGTH) continue;

                    // Get current node position
                    var lastNode = currentPath.Last();
                    var currentPoint = lastNode.IdPuntoUno == (currentPath.Count > 1 ? GetLastVisitedPoint(currentPath) : idActualPunto)
                        ? lastNode.IdPuntoDue
                        : lastNode.IdPuntoUno;

                    // Check if reached destination
                    if (lastNode.IdPuntoUno == idDestinationPunto || lastNode.IdPuntoDue == idDestinationPunto)
                        return currentPath;
                    

                    // Find possible next moves
                    var possibleMoves = adiacenze.Where(a =>
                        (a.IdPuntoUno == currentPoint || a.IdPuntoDue == currentPoint) &&
                        a.Abilitata &&
                        !currentPath.Contains(a) &&
                        !LeadsToVisitedPoint(a, currentPath, currentPoint)
                    ).ToList();

                    if (possibleMoves.Any())
                    {
                        // First move replaces current path
                        var firstMove = possibleMoves.First();
                        var newPath = new List<Adiacenza>(currentPath) { firstMove };
                        paths[i] = newPath;

                        // Additional moves create new paths
                        for (int j = 1; j < possibleMoves.Count; j++)
                        {
                            var additionalPath = new List<Adiacenza>(currentPath) { possibleMoves[j] };
                            paths.Add(additionalPath);
                        }
                    }
                    else
                    {
                        // Dead end - remove path
                        paths.RemoveAt(i);
                        i--;
                        currentPathsCount--;
                    }
                }
            }

            return shortestPath ?? new List<Adiacenza>();
        }

        private int GetLastVisitedPoint(List<Adiacenza> path)
        {
            var secondLastNode = path[path.Count - 2];
            var lastNode = path.Last();
            return lastNode.IdPuntoUno == secondLastNode.IdPuntoUno || lastNode.IdPuntoUno == secondLastNode.IdPuntoDue
                ? lastNode.IdPuntoUno
                : lastNode.IdPuntoDue;
        }

        private bool LeadsToVisitedPoint(Adiacenza move, List<Adiacenza> path, int currentPoint)
        {
            var nextPoint = move.IdPuntoUno == currentPoint ? move.IdPuntoDue : move.IdPuntoUno;
            return path.Any(p => p.IdPuntoUno == nextPoint || p.IdPuntoDue == nextPoint);
        }

        private bool NemiciNellaTessera(int idActualPunto)
        {
            Punto punto = _game.GetPuntoById(idActualPunto);
            var tessera = punto.LocatedIn;
            var personaggiNellaTessera = _game.GetPersonaggiLocatedIn(tessera);
            var ciSonoNemiciNellaTessera = personaggiNellaTessera.Count(p => p.TipoPersonaggio == TipoPersonaggio.NemicoNPC
                                                                          || p.TipoPersonaggio == TipoPersonaggio.NemicoPersonaggio);
            return ciSonoNemiciNellaTessera > 0;
        }
    }
}