using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primitives
{
    public class Mappa
    {
        public int Id {  get; set; }
        public string Descrizione { get; set; } = string.Empty;

        public IEnumerable<Area> Aree { get; internal set; }

        public Mappa(int id, string descrizione, IEnumerable<Area> aree)
        {
            Id = id;
            Descrizione = descrizione;
            Aree = aree;

            foreach (var area in aree)
                area.LocatedIn = this;
        }
    }
}
