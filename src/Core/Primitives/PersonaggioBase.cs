namespace Core.Primitives
{
    public class PersonaggioBase
    {
        public PersonaggioBase(int id, string nome, int puntiVitaMassimi, int attacco, int difesa, string descrizione)
        {
            Id = id;
            Nome = nome;
            PuntiVitaMassimi = puntiVitaMassimi;
            Attacco = attacco;
            Difesa = difesa;
            Descrizione = descrizione;
        }

        public PersonaggioBase() { }

        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int PuntiVitaMassimi { get; set; }
        public int Attacco { get; set; }
        public int Difesa { get; set; }
        public string Descrizione { get; set; } = string.Empty;

    }

}