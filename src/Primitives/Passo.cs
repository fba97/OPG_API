using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primitives
{
    public class Passo
    {
        public int Id { get; set; }
        public int Sorgente { get; set; }
        public int Destinazione { get; set; }
        public Missione? Missione { get; set; } = null;
        public int Stato { get; set; } = 0;
    }
}
