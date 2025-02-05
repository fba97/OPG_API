using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primitives
{
    public enum StatoOggetto : int
    {
        Nuovo = 1,
        Usato = 2,
        Attivato = 3, //questo per effetti continui ad attivazione singola
        Equipaggiato = 4 // questo è per gli oggetti che hanno bonus che si usano solo su determinate condizioni
    }
}
