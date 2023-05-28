using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primitives
{
    public class Partita : IRepositoryItem, IUniqueCodeRepositoryItem
    {
        public Partita() { }

        public Partita(int id, string nome, int idObiettivo, int difficolta, int statoPartita, DateTime? dataInizioPartita, DateTime? dataFinePartita, IEnumerable<int> personaggiInGioco, IEnumerable<int> oggettiInGioco)
        {
            Id = id;
            Nome = nome;
            IdObiettivo = idObiettivo;
            Difficolta = difficolta;
            StatoPartita = statoPartita;
            DataInizioPartita = dataInizioPartita;
            DataFinePartita = dataFinePartita;
            PersonaggiInGioco = personaggiInGioco;
            OggettiInGioco = oggettiInGioco;
        }

        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int IdObiettivo { get; set; } = 0; //per il momento rimane a 0 
        public int Difficolta { get; set; } = 0; //per il momento rimane a 0 
        public int StatoPartita { get; set; } = 0; // 1 nuova, 2 esecuzione, 3 terminata
        public DateTime? DataInizioPartita { get; set; }
        public DateTime? DataFinePartita { get; set; }
        public IEnumerable<int> PersonaggiInGioco { get; set; } = new List<int>();
        public IEnumerable<int> OggettiInGioco { get; set; } = new List<int>();

        public string Code => "";
    }
}