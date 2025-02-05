using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primitives
{
    public enum TipoAzione : int
    {
        Spostamento = 1,
        Attacco = 2,
        Raccogli = 3,
        Scambia = 4,
        Fuggi = 5,
        Vendi = 6,
        Usa = 7,
        Imprevisto = 8,
        Probabilita = 9
    }
}
