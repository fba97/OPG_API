using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primitives
{
    public class Area
    {

        public int Id { get; set; }
        public string Descrizione { get; set; } = string.Empty;
        public IEnumerable<Tessera> Tessere { get; internal set; }

        public Mappa LocatedIn { get; internal set; } = null!;

        public Mappa Parent => LocatedIn;
        public Area(int id, string descrizione, IEnumerable<Tessera> tessere)
        {
            Id = id;
            Descrizione = descrizione;
            Tessere = tessere;

            foreach (var tessera in tessere)
                tessera.LocatedIn = this;
        }

    }
}
