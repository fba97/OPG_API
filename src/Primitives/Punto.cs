using System.Text.Json.Serialization;

namespace Primitives
{
    public class Punto
    {
        public int Id { get; set; }
        public int Id_Tessera { get; set; }
        public string Descrizione { get; set; } = string.Empty;
        public int Capienza { get; }
        public bool Blocco { get; set; }
        [JsonIgnore]
        public Tessera LocatedIn { get; internal set; } = null!;

        [JsonIgnore]
        public Tessera Parent => LocatedIn;

        public Punto(int id, int id_Tessera, string descrizione, int capienza = 1, bool blocco = false)
        {
            Id = id;
            Id_Tessera = id_Tessera;
            Descrizione = descrizione;
            Capienza = capienza;
            Blocco = blocco;
        }
    }
}