using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primitives
{
    public class SpostaOggetto : Azione
    {
        public SpostaOggetto(Personaggio soggetto, Oggetto oggetto, TipoAzione tipoSpostamento)
        {
            Personaggio = soggetto;
            TipoAzione = tipoSpostamento;
            Oggetto = oggetto;
        }

        public Oggetto? Oggetto { get; set; }
    }
}