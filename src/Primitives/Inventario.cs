using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primitives
{
    public class Inventario
    {
        public int Id { get; set; }
        public int IdPersonaggioPartita { get; set; }
        public List<int> ListaIdOggetti { get; set; } = new List<int>();    

    }
}