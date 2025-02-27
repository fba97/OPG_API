﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primitives
{
    public class UsaOggetto : Azione
    {
        public UsaOggetto(Personaggio chiUsa, Oggetto oggetto)
        {
            Personaggio = chiUsa;
            TipoAzione = TipoAzione.Usa;
            Oggetto = oggetto;
        }

        public Oggetto? Oggetto { get; set; }
    }
}