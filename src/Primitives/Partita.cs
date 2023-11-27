using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primitives
{
    public class InfoPartita
    {
        public InfoPartita() { }

        public int Id { get; set; } = 0;
        public string Nome { get; set; } = string.Empty;
        public int IdObiettivo { get; set; } = 0; //per il momento rimane a 0 
        public int Difficolta { get; set; } = 0; //per il momento rimane a 0 
        public int StatoPartita { get; set; } = 0; // 1 nuova, 2 esecuzione, 3 terminata
        public DateTime? DataInizioPartita { get; set; }
        public DateTime? DataFinePartita { get; set; }
    }
}