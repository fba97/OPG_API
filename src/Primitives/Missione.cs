using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primitives
{
    public class Missione : Azione
    {
        public Missione(Personaggio soggetto, int puntoDestinazione) 
        {
            Personaggio = soggetto;
            Partenza = soggetto.Posizione;
            Destinazione = puntoDestinazione;
            TipoAzione = TipoAzione.Spostamento; 
        }

        public int Partenza { get; set; }
        public int Destinazione { get; set; }
        public StatoMissione Stato { get; set; } = StatoMissione.Nuova;
        public IEnumerable<Passo> Passi { get; set; } = new List<Passo>();
    }
}
