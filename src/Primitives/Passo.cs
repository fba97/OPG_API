using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primitives
{
    public class Passo
    {
        public Passo(int id, int sorgente, int destinazione, Missione? missione, StatoPasso stato)
        {
            Id = id;
            Sorgente = sorgente;
            Destinazione = destinazione;
            Missione = missione;
            Stato = stato;
        }

        public int Id { get; set; }
        public int Sorgente { get; set; }
        public int Destinazione { get; set; }
        public Missione? Missione { get; set; } = null;
        public StatoPasso Stato { get; set; } = StatoPasso.Nuovo;

        public StatoPasso SetStato(StatoPasso stato)
        {
            Stato = Stato;
            return Stato;
        }

    }
}
