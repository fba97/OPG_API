using Core.Game_dir;
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
        private Game _game;
        private IEnumerable<Inventario> _inventari;
        private IEnumerable<Inventario> Inventari
        {
            get
            {
                if (_inventari == null && _game.PartitaAttuale != null)
                {
                    _inventari = _game.PartitaAttuale.Inventari;
                }
                return _inventari ?? Enumerable.Empty<Inventario>();
            }
        }

        public OggettoManager(Game game)
        {
            _game = game;
        }


        public void AggiungiOggetto(Oggetto oggetto, Personaggio personaggioRicevente)
        {
            // Logica per aggiungere un oggetto all'inventario
        }

        public void RimuoviOggetto(int itemId)
        {
            // Logica per rimuovere un oggetto dall'inventario
        }

        public void RaccogliOggetto()
        {

        }

        public void ScambiaOggetto(int itemId)
        {

        }

        public void VendiOggetto(int itemId)
        {

        }
        

        public void UsaOggetto(UsaOggetto oggetto)
        {
            // Logica per usare un oggetto
        }

        public void EquipaggiaOggetto(int itemId)
        {
            // Logica per equipaggiare un oggetto
        }























    }
}
