using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primitives
{
    public class Inventario
    {
        public Inventario() { }
        public Inventario(int id) 
        {
            Id = id;
        }
        public Inventario(int id, IEnumerable<Oggetto> listaIdOggetti) 
        {
            Id = id;
            ListaIdOggetti = listaIdOggetti;
        }


        public int Id { get; set; } = 0;
        public IEnumerable<Oggetto> ListaIdOggetti { get; set; } = new List<Oggetto>();

    }
}