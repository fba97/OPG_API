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
        public Inventario(int id, int idPersonaggio) 
        {
            Id = id;
            IdPersonaggio = idPersonaggio;
        }
        public Inventario(int id, int idPersonaggio, IEnumerable<Oggetto> listaIdOggetti) 
        {
            Id = id;
            IdPersonaggio = idPersonaggio;
            ListaIdOggetti = listaIdOggetti;
        }


        public int Id { get; set; } = 0;
        public int IdPersonaggio { get; set; }
        public IEnumerable<Oggetto> ListaIdOggetti { get; set; } = new List<Oggetto>();

    }
}