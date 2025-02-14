using Primitives.Oggetti;
using Primitives.Oggetti.Effetti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Primitives;

    public class Oggetto
    {
        public Oggetto(int id, string nome, string descrizione, int tipo, int stato, int bonusAttacco, int bonusDifesa, int? id_Posizione = null, int? id_inventario = null, EffettoOggetto? effettoOggetto = null)
        {
            Id = id;
            Nome = nome;
            Descrizione = descrizione;
            Tipo = (TipoOggetto) tipo;
            Stato = (StatoOggetto) stato;
            BonusAttacco = bonusAttacco;
            BonusDifesa = bonusDifesa;
            Id_Posizione = id_Posizione;
            Id_Inventario = id_inventario;
            if (effettoOggetto is not null)
                Effetto = effettoOggetto;
        }

    public Oggetto(int id, string nome, string descrizione, int tipo, int stato, int bonusAttacco, int bonusDifesa, string tipoEffetto, string configurazione, int? id_Posizione = null, int? id_inventario = null)
    {
        Id = id;
        Nome = nome;
        Descrizione = descrizione;
        Tipo = (TipoOggetto)tipo;
        Stato = (StatoOggetto)stato;
        BonusAttacco = bonusAttacco;
        BonusDifesa = bonusDifesa;
        Id_Posizione = id_Posizione;
        Id_Inventario = id_inventario;
        Effetto = EffettoOggettoFactory.CreaEffetto(tipoEffetto, configurazione);
    }
    public Oggetto() { }
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descrizione { get; set; } = string.Empty;
        public TipoOggetto Tipo { get; set; }
        public StatoOggetto Stato {  get; set; }
        public int BonusAttacco { get; set; }
        public int BonusDifesa { get; set; }
        public int? Id_Posizione { get; set; }
        public int? Id_Inventario { get; set; }

    public EffettoOggetto Effetto { get; set; } = new NessunEffettoOggetto();

    }

