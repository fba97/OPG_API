namespace Primitives
{
    public class PersonaggioBase
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int PuntiVitaMassimi { get; set; }
        public int Attacco { get; set; }
        public int Difesa { get; set; }
        public string Descrizione { get; set; } = string.Empty;

    }

}