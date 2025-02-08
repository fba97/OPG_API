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
        public Inventario(int id, int? idPersonaggio) 
        {
            Id = id;
            PersonaggioId = idPersonaggio;
        }
        public Inventario(int id, int? idPersonaggio, List<OggettoInventario> listaIdOggetti) 
        {
            Id = id;
            PersonaggioId = idPersonaggio;
            Oggetti = listaIdOggetti;
        }
            public int Id { get; set; }
            public int? PersonaggioId { get; set; }  // Null per inventari non legati a personaggi (es. casse)
            public int CapacitaMassima { get; set; } = 10;
            public TipoInventario Tipo { get; set; } = TipoInventario.Personaggio;
            public List<OggettoInventario> Oggetti { get; set; } = new List<OggettoInventario>();

         
        }
    public class OggettoInventario
    {
        public Oggetto Oggetto { get; set; }
        public int Quantita { get; set; }
        public bool IsEquipaggiato { get; set; }

    }

}
