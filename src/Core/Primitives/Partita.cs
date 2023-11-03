using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Primitives
{
    public class Partita : IRepositoryItem, IUniqueCodeRepositoryItem
    {
        public Partita() { }

        public Partita(int id,int id_Giocatore, string nome, int idObiettivo, int difficolta, int statoPartita, DateTime? dataInizioPartita, DateTime? dataFinePartita, DateTime? dataUltimoSalvataggio, IEnumerable<int> oggettiInGioco = null, IEnumerable<int> personaggiInGioco = null)
        {
            Id = id;
            Id_Giocatore = id_Giocatore;
            Nome = nome;
            IdObiettivo = idObiettivo;
            Difficolta = difficolta;
            StatoPartita = statoPartita;
            DataInizioPartita = dataInizioPartita;
            DataFinePartita = dataFinePartita;
            DataUltimoSalvataggio = dataUltimoSalvataggio;
            PersonaggiInGioco = personaggiInGioco ?? Array.Empty<int>();
            OggettiInGioco = oggettiInGioco ?? Array.Empty<int>();
        }
        public int Id { get; set; }
        public int Id_Giocatore { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int IdObiettivo { get; set; } = 0; //per il momento rimane a 0 
        public int Difficolta { get; set; } = 0; //per il momento rimane a 0 
        public int StatoPartita { get; set; } = 0; // 1 nuova, 2 esecuzione, 3 terminata
        public DateTime? DataInizioPartita { get; set; }
        public DateTime? DataUltimoSalvataggio { get; set; }
        public DateTime? DataFinePartita { get; set; }
        public IEnumerable<int> PersonaggiInGioco { get; set; } = new List<int>();
        public IEnumerable<int> OggettiInGioco { get; set; } = new List<int>();

        public string Code => "";
    }
}