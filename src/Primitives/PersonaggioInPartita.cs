﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primitives
{
    public class PersonaggioInPartita : PersonaggioBase 
    {
        public PersonaggioInPartita(int id, string nome, int puntiVitaMassimi, int attacco, int difesa, string descrizione, int idPersonaggioPartita, int idPartita, int livello, int tipoPersonaggio, int posizioneXPersonaggio, int posizioneYPersonaggio, int puntiVitaPersonaggio, int attacco_InPartita, int difesa_InPartita, Stato stato, int taglia) :base(id, nome, puntiVitaMassimi, attacco, difesa, descrizione)
        {
            IdPersonaggioPartita = idPersonaggioPartita;
            IdPartita = idPartita;
            Livello = livello;
            TipoPersonaggio = tipoPersonaggio;
            PosizioneXPersonaggio = posizioneXPersonaggio;
            PosizioneYPersonaggio = posizioneYPersonaggio;
            PuntiVitaPersonaggio = puntiVitaPersonaggio;
            Attacco_InPartita = attacco_InPartita;
            Difesa_InPartita = difesa_InPartita;
            Stato = stato ?? new Stato();
            Taglia = taglia;
        }

        public PersonaggioInPartita() : base() { }

        public int IdPersonaggioPartita { get; set; }
        public int IdPartita { get; set; }
        public int Livello { get; set; }
        public int TipoPersonaggio { get; set; }
        public int PosizioneXPersonaggio { get; set; }
        public int PosizioneYPersonaggio { get; set; }
        public int PuntiVitaPersonaggio { get; set; }
        public int Attacco_InPartita { get; set; }
        public int Difesa_InPartita { get; set; }
        public Stato Stato { get; set; } = new Stato();
        public int Taglia { get; set; }

  
        //public virtual PersonaggioBase PersonaggioBase { get; set; }
    }

}
