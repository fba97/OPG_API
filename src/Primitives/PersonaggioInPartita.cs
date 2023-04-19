using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primitives
{
    public class PersonaggioInPartita : PersonaggioBase
    {
        public int IdPersonaggioPartita { get; set; }
        public int IdPartita { get; set; }
        public int Livello { get; set; }
        public int TipoPersonaggio { get; set; }
        public int PosizioneXPersonaggio { get; set; }
        public int PosizioneYPersonaggio { get; set; }
        public int PuntiVitaPersonaggio { get; set; }
        public int Attacco_InPartita { get; set; }
        public int Difesa_InPartita { get; set; }
        public Stato Stato { get; set; } = new Stato();
        public int Taglia { get; set; }


        //public virtual PersonaggioBase PersonaggioBase { get; set; }
    }

}
