using Primitives.Oggetti.Effetti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primitives.Oggetti;

public class EffettoOggettoFactory
{
    public static EffettoOggetto CreaEffetto(string tipoEffetto, string configurazione)
    {
        // Gestione del caso nullo o vuoto
        if (string.IsNullOrEmpty(tipoEffetto))
            return new NessunEffettoOggetto();

        // Parsing della configurazione in base al tipo di effetto
        return tipoEffetto.ToLower() switch
        {
            "moltiplicatoreattacco" => CreaMoltiplicatoreAttacco(configurazione),
            "nessuneffetto" => new NessunEffettoOggetto(),
            // Aggiungine gli altri. li trovi nella tabella oggetti del db.
            

            _ => new NessunEffettoOggetto()
        };
    }

    private static EffettoOggetto CreaMoltiplicatoreAttacco(string configurazione)
    {
        if (!int.TryParse(configurazione, out int moltiplicatore))
        {
            throw new ArgumentException($"Configurazione non valida per MoltiplicatoreAttacco: {configurazione}");
        }

        return new MoltiplicatoreAttacco(moltiplicatore);
    }

    // Aggiungi qui altri metodi privati per la creazione di specifici effetti
    // Esempio:
    /*
    private static EffettoOggetto CreaEffettoTemporaneo(string configurazione)
    {
        var parametri = configurazione.Split(',');
        if (parametri.Length != 2)
            throw new ArgumentException("La configurazione deve contenere durata e valore");

        if (!int.TryParse(parametri[0], out int durata) || 
            !int.TryParse(parametri[1], out int valore))
            throw new ArgumentException("Parametri non validi");

        return new EffettoTemporaneo(durata, valore);
    }
    */
}