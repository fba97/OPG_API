using Primitives.Oggetti.Effetti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primitives
{
    public class OggettoTemplate
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descrizione { get; set; }
        public TipoOggetto Tipo { get; set; }
        public StatoOggetto Stato { get; set; }
        public int BonusAttacco { get; set; }
        public int BonusDifesa { get; set; }
        public EffettoOggetto Effetto { get; set; } = new NessunEffettoOggetto(); 
    }
}
