using Core.Game_dir;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core.Map_Handling.Managers
{
    public class ActionManager
    {
        private readonly Dictionary<TipoAzione, IActionHandler> _handlers;
        private Game _game;
        public ActionManager(Game game, IServiceProvider services)
        {
            _game = game;
            _handlers = new Dictionary<TipoAzione, IActionHandler> 
        {
            { TipoAzione.Spostamento, new MissionHandler(game) },
            { TipoAzione.Attacco, new AttaccoHandler(game) },
            { TipoAzione.Raccogli, new SpostaOggettoHandler(game,services) },
            { TipoAzione.Usa, new UsaOggettoHandler(game,services) }

            // Altri handler...
        };
        }

        public ActionResult ExecuteAction(Azione azione)
        {
            var partitaAttuale = _game.PartitaAttuale;

            if (partitaAttuale is null)
                return new ObjectResult(new { message = "Caricare o creare una partita prima di fare qualsiasi azione." }) { StatusCode = 500 };

            var turno = partitaAttuale.ActualTurno;

            if(ConsumeAction(turno, azione.PesoAzione))
                return _handlers[azione.TipoAzione].Execute(azione);
            else
                return  new ObjectResult(new { message = "Hai finito le azioni disponibili." }) { StatusCode = 500 };
        }
    
    public bool IsActionPossible(Turno actTurn,int actionCost)
        {
            return actTurn.GiocoIniziato && actionCost <= actTurn.AzioniRimanenti;
        }

        public bool ConsumeAction(Turno actTurn, int actionCost)
        {
            if (!IsActionPossible(actTurn,actionCost))
                return false;

            actTurn.AzioniRimanenti -= actionCost;
            return true;
        }

        public void EndTurn(Turno actTurn)
        {
            if (!actTurn.GiocoIniziato) return;

            var currentIndex = actTurn.PersonaggiIds.IndexOf(actTurn.IdDelPersonaggioInTurno);
            var nextIndex = (currentIndex + 1) % actTurn.PersonaggiIds.Count;

            actTurn = new Turno(
                personaggiIds: actTurn.PersonaggiIds,
                idDelPersonaggioInTurno: actTurn.PersonaggiIds[nextIndex],
                turnoCorrente: nextIndex == 0 ? actTurn.TurnoCorrente + 1 : actTurn.TurnoCorrente,
                giocoIniziato: true,
                azioniMassimePerTurno: actTurn.AzioniMassimePerTurno
            );

            actTurn.AzioniRimanenti = actTurn.AzioniMassimePerTurno;

            _game.PartitaAttuale.ActualTurno = actTurn;
        }

        public Turno Reset(Turno actTurn)
        {
            var resettedTurn = new Turno();
            resettedTurn.PersonaggiIds = actTurn.PersonaggiIds;
            return resettedTurn;
        }
    }
}
