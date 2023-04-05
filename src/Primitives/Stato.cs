using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primitives
{
    public class Stato
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;    
        public string Descrizione { get; set; } = string.Empty;
        public string Effetto { get; set; } = string.Empty;
        public int DurataTurni { get; set; }
        public int IdPersonaggioPartita { get; set; }

        //public virtual PersonaggioInPartita Personaggio { get; set; }
    }
}
