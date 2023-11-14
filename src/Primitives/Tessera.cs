namespace Primitives
{
    public class Tessera
    {
        public int Id { get; set; }
        public int Id_Area { get; set; }
        public string Descrizione { get; set; } = string.Empty;
        public string Tipo { get; } = string.Empty;
        public bool Configurazione { get; set; }

        public Area LocatedIn { get; internal set; } = null!;

        public Area Parent => LocatedIn;
        public IEnumerable<Punto> Punti { get; internal set; }

        public Tessera(int id, int id_Area, string descrizione, IEnumerable<Punto> punti)
        {
            Id = id;
            Id_Area = id_Area;
            Descrizione = descrizione;
            Punti = punti;

            foreach (var punto in punti)
                punto.LocatedIn = this;
        }
    }
}