using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Primitives
{
    public class Area
    {

        public int Id { get; set; }
        public int Id_Mappa { get; set; }
        public string Descrizione { get; set; } = string.Empty;
        public IEnumerable<Tessera> Tessere { get; internal set; }
        [JsonIgnore]
        public Mappa LocatedIn { get; internal set; } = null!;
        [JsonIgnore]
        public Mappa Parent => LocatedIn;
        public Area(int id, int id_mappa, string descrizione, IEnumerable<Tessera> tessere)
        {
            Id = id;
            Id_Mappa = id_mappa;
            Descrizione = descrizione;
            Tessere = tessere;

            foreach (var tessera in tessere)
                tessera.LocatedIn = this;
        }

    }
}
