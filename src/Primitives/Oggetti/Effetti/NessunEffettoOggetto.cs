using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Primitives.Oggetti.Effetti;


    // Effetto che aumenta l'attacco permanentemente
public class NessunEffettoOggetto : EffettoOggetto
{
    public NessunEffettoOggetto()
    {
        Nome = "";
        Descrizione = "";
    }
    public override Result<bool> CondizioniPerUso(Azione azione) => Result.Success(true);
    public override void Usa(Azione azione){}
}
