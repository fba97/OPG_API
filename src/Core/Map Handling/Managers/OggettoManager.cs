using Core.Game_dir;
using Microsoft.Extensions.DependencyInjection;
using Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Map_Handling.Managers
{
    public class OggettoManager
    {
        private readonly Game _game;
        private readonly InventarioManager _inventarioManager;

        public OggettoManager(Game game, InventarioManager inventarioManager)
        {
            _game = game;
            _inventarioManager = inventarioManager;
        }

        public Result<bool> RaccogliOggetto(SpostaOggetto azione)
        {
            if(azione is null)
                return Result.Failure<bool>("azione nulla");

            if (azione.Oggetto is null)
                return Result.Failure<bool>("oggetto nullo");

            var personaggio = _game.PartitaAttuale?.Personaggi.FirstOrDefault(p => p.Id == azione.Personaggio.Id);
            if (personaggio == null)
                return Result.Failure<bool>("Personaggio non trovato");

            var inventarioDestinazione = _game.PartitaAttuale.Inventari.FirstOrDefault(i => i.PersonaggioId == personaggio.Id);

            // Verifica distanza di raccolta
            if (!IsInRangeForPickup(personaggio.Posizione, azione.Oggetto.Id_Posizione))
                return Result.Failure<bool>("Oggetto troppo lontano");

            return _inventarioManager.SpostaOggetto(
                azione.Oggetto.Id,
                inventarioOrigineId: 0, //standard per l'inventario mappa
                inventarioDestinazioneId: inventarioDestinazione.Id
            );
        }


        public void AggiungiOggetto(Oggetto oggetto, Personaggio personaggioRicevente)
        {

        }

        public void RimuoviOggetto(int itemId)
        {
            // Logica per rimuovere un oggetto dall'inventario
        }


        public void ScambiaOggetto(int itemId)
        {

        }

        public void VendiOggetto(int itemId)
        {

        }


        public Result<bool> UsaOggetto(UsaOggetto usaOggetto)
        {
            if(usaOggetto is null)
                return Result.Failure<bool>("azione nulla");

            var oggetto = usaOggetto.Oggetto;
            if(oggetto is null)
                return Result.Failure<bool>("oggetto nullo");

            var personaggio = _game.PartitaAttuale?.Personaggi.FirstOrDefault(p => p.Id == usaOggetto.Personaggio.Id);
            if(personaggio == null)
                return Result.Failure<bool>("Personaggio non trovato");

            var condizioniResult = oggetto.Effetto.CondizioniPerUso(usaOggetto);
            if(!condizioniResult.IsSuccess)
                return condizioniResult;

            oggetto.Effetto.Usa(usaOggetto);

            return Result.Success(true);
        }

        public void EquipaggiaOggetto(int itemId)
        {
            // Logica per equipaggiare un oggetto
        }

        private bool IsInRangeForPickup(int posizionePersonaggio, int? posizioneOggetto)
        {
            if (!posizioneOggetto.HasValue) return false;
            // Implementa la tua logica di distanza qui
            return true;
        }
    }
}
