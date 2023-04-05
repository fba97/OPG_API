using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primitives
{
        public class Partita
        {
            public int Id { get; set; }
            public int IdGiocatore { get; set; }
            public string StatoPartita { get; set; } = string.Empty;
        public int? IdPersonaggioPartita { get; set; }
            public int? IdMostro { get; set; }
            public int PosizioneX { get; set; }
            public int PosizioneY { get; set; }
            public int PuntiVita { get; set; }
            public DateTime TimestampUltimoSalvataggio { get; set; } = DateTime.MinValue;
        }
}