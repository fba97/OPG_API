using Core.Game_dir;
using Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Map_Handling.Managers
{
    public class InventarioManager
    {
        private readonly Game _game;
        private readonly Dictionary<int, Inventario> _inventariCache = new();

        public InventarioManager(Game game)
        {
            _game = game;
        }

        public Result<Inventario> GetInventario(int inventarioId)
        {
            if (_inventariCache.TryGetValue(inventarioId, out var cachedInventario))
                return Result.Success(cachedInventario);

            if(_game.PartitaAttuale is null)
                return Result.Failure<Inventario>("La partita non è stata caricata. Non è possibile prendere inventari.");

            var inventario = _game.PartitaAttuale.Inventari.FirstOrDefault(i => i.Id == inventarioId);
            if (inventario == null)
                return Result.Failure<Inventario>("Inventario non trovato");

            _inventariCache[inventarioId] = inventario;
            return Result.Success(inventario);
        }

        public Result<bool> SpostaOggetto(int oggettoId, int inventarioOrigineId, int inventarioDestinazioneId, int quantita = 1)
        {
            var origineResult = GetInventario(inventarioOrigineId);
            var destinazioneResult = GetInventario(inventarioDestinazioneId);

            if (!origineResult.IsSuccess || !destinazioneResult.IsSuccess)
                return Result.Failure<bool>("Almeno un inventario non trovato");

            var origine = origineResult.Value;

            if (origine is null)
                return Result.Failure<bool>("Inventario origine non trovato");
            
            var destinazione = destinazioneResult.Value;

            if(destinazione is null)
                return Result.Failure<bool>("Inventario destinazione non trovato");

            // Verifica limiti di capacità
            if (destinazione.Oggetti.Count >= destinazione.CapacitaMassima)
                return Result.Failure<bool>("Inventario destinazione pieno");

            var oggetto = origine.Oggetti.First(o => o.Oggetto.Id == oggettoId);
            origine.Oggetti.Remove(oggetto);
            destinazione.Oggetti.Add(oggetto);

            return Result.Success(true);
        }

        public Result<bool> UsaOggetto(int oggettoId, int inventarioId, int personaggioId)
        {
            var inventarioResult = GetInventario(inventarioId);
            if (!inventarioResult.IsSuccess)
                return Result.Failure<bool>("Inventario non trovato");

            var inventario = inventarioResult.Value;

            // Verifica che l'oggetto appartenga al personaggio giusto
            if (inventario.PersonaggioId != personaggioId)
                return Result.Failure<bool>("L'oggetto non appartiene a questo personaggio");

            // Logica di utilizzo
            // ...

            return Result.Success(true);
        }
    }
}
