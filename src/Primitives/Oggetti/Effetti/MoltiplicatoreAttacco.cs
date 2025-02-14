using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Primitives.Oggetti.Effetti;


    // Effetto che aumenta l'attacco permanentemente
public class MoltiplicatoreAttacco : EffettoOggetto
{
    private int Moltiplicatore = 1;


    public MoltiplicatoreAttacco(int moltiplicatore)
    {
        Nome = "Moltiplicatore";
        Descrizione = $"Moltiplica l'attacco di {moltiplicatore} volte permanentemente";
        Moltiplicatore = moltiplicatore;
    }
    public override Result<bool> CondizioniPerUso(Azione azione)
    {
        // solo se sei zoro in realtà
        return Result.Success(true);
    }

    public override void Usa(Azione azione)
    {
        azione = (UsaOggetto)azione;
        azione.Personaggio.Attacco *= Moltiplicatore;
    }
}
