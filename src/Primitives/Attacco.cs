using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primitives
{
    public class Attacco : Azione
    {
        public Attacco(Personaggio difensore, Personaggio attaccante)
        {
            TipoAzione = TipoAzione.Attacco;
            Difensore = difensore;
            Personaggio = attaccante;
        }

        public Personaggio Difensore { get; set; }
        public TipoAttacco TipoAttacco { get; set; } = TipoAttacco.AttaccoSingolo;
        public StatoAttacco Stato { get; set; } = StatoAttacco.Nuovo; 
        
    }
}
