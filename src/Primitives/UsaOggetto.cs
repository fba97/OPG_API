using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primitives
{
    public class UsaOggetto : Azione
    {
        public UsaOggetto(Personaggio chiUsa)
        {
            Personaggio = chiUsa;
            TipoAzione = TipoAzione.Usa;
        }

        public Oggetto? Oggetto { get; set; }
    }
}