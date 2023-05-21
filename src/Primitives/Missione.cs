using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primitives
{
    public class Missione
    {
        public int Id { get; set; }
        public int TipoMissione { get; set; }
        public int Partenza { get; set; }
        public int Destinazione { get; set; }
        public IEnumerable<Passo> Passi { get; set; }    
    }
}
