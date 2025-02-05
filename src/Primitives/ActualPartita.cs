using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Primitives
{
    public class ActualPartita
    {

        public ActualPartita() { }

        public ActualPartita(int id, string nome, int idGiocatore, int idObiettivo, int difficolta, int statoPartita,Turno turno, DateTime? dataInizioPartita,DateTime? dataUltimoSalvataggio, DateTime? dataFinePartita, string json)
        {
            Id = id;
            Nome = nome;
            IdGiocatore = idGiocatore;
            IdObiettivo = idObiettivo;
            Difficolta = difficolta;
            StatoPartita = statoPartita;
            ActualTurno = turno;
            DataInizioPartita = dataInizioPartita;
            DataUltimoSalvataggio = dataUltimoSalvataggio;
            DataFinePartita = dataFinePartita;
            JSONSalvataggio = json;
        }

        public ActualPartita(int id, string nome, int idGiocatore, int idObiettivo, int difficolta, int statoPartita, Turno turno,DateTime? dataInizioPartita, DateTime? dataUltimoSalvataggio, DateTime? dataFinePartita, string jSONSalvataggio, Mappa? mappa, IEnumerable<Area> aree, IEnumerable<Tessera> tessere, IEnumerable<Punto> punti, IEnumerable<Personaggio> personaggi, IEnumerable<Oggetto> oggetti, IEnumerable<Adiacenza> adiacenze, IEnumerable<Inventario> inventari, IEnumerable<Combattimento> combattimenti, IEnumerable<Missione> missioni) : this(id, nome, idGiocatore, idObiettivo, difficolta, statoPartita, turno, dataInizioPartita, dataUltimoSalvataggio, dataFinePartita, jSONSalvataggio)
        {
            Mappa = mappa;
            Aree = aree;
            Tessere = tessere;
            Punti = punti;
            Personaggi = personaggi;
            Oggetti = oggetti;
            Adiacenze = adiacenze;
            Inventari = inventari;
            Combattimenti = combattimenti;
            Missioni = missioni;
        }

        public string Serialize()
        {
            return JsonSerializer.Serialize(this);
        }

        public static ActualPartita Deserialize(string json)
        {
            return JsonSerializer.Deserialize<ActualPartita>(json) ?? new ActualPartita();
        }

        public int Id { get; set; } = 0;
        public string Nome { get; set; } = string.Empty;
        public int IdGiocatore { get; set; } = 0; //per il momento rimane a 0 
        public int IdObiettivo { get; set; } = 0; //per il momento rimane a 0 
        public int Difficolta { get; set; } = 0; //per il momento rimane a 0 
        public int StatoPartita { get; set; } = 0; // 1 nuova, 2 esecuzione, 3 terminata
        public Turno ActualTurno { get; set; } = new Turno();
        public DateTime? DataInizioPartita { get; set; }
        public DateTime? DataUltimoSalvataggio { get; set; }        
        public DateTime? DataFinePartita { get; set; }
        public string JSONSalvataggio { get; set; } = string.Empty;


        public Mappa? Mappa { get; set; } = null;
        public IEnumerable<Area> Aree { get; set; } = new List<Area>();
        public IEnumerable<Tessera> Tessere { get; set; } = new List<Tessera>();
        public IEnumerable<Punto> Punti { get; set; } = new List<Punto>();
        public IEnumerable<Personaggio> Personaggi { get; set; } = new List<Personaggio>();
        public IEnumerable<Oggetto> Oggetti { get; set; } = new List<Oggetto>();
        public IEnumerable<Adiacenza> Adiacenze { get; set; } = new List<Adiacenza>();

        public IEnumerable<Inventario> Inventari { get; set; } = new List<Inventario>();
        public IEnumerable<Combattimento> Combattimenti { get; set; } = new List<Combattimento>();
        public IEnumerable<Missione> Missioni { get; set; } = new List<Missione>();

    }
}