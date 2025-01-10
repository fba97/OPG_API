namespace Primitives
{
    public class Personaggio
    {


        public Personaggio() { }

        public Personaggio(int id, string nome, int punti_Vita, int attacco, int difesa, string descrizione, int tipoPersonaggio, int posizione, int taglia, int livello, Stato? stato = null)
        {
            Id = id;
            Nome = nome;
            Punti_Vita = punti_Vita;
            Attacco = attacco;
            Difesa = difesa;
            Descrizione = descrizione;

            if (Enum.IsDefined(typeof(TipoPersonaggio), tipoPersonaggio))
                TipoPersonaggio = (TipoPersonaggio)tipoPersonaggio;
            else
                throw new ArgumentException($"Il valore {tipoPersonaggio} non è valido per l'enumerazione TipoPersonaggio.");
            
            Posizione = posizione;
            Stato = stato;
            Taglia = taglia;
            Livello = livello;
        }

        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Punti_Vita { get; set; }
        public int Attacco { get; set; }
        public int Difesa { get; set; }
        public string Descrizione { get; set; } = string.Empty;
        public TipoPersonaggio TipoPersonaggio { get; set; }
        public int Posizione { get; set; }
        public int Taglia { get; set; }
        public int Livello { get; set; }
        public Inventario Inventario { get; set; } = new Inventario();  
        public Stato? Stato { get; set; }

        public int SetPosizione(int nuovaPosizione)
        {
            Posizione = nuovaPosizione;
            return Posizione;
        }
    }

}