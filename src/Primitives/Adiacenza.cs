using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primitives
{
    public class Adiacenza
    {
        public Adiacenza(int id, int idPuntoUno, int idPuntoDue, bool bidirezionale, bool abilitata)
        {
            Id = id;
            IdPuntoUno = idPuntoUno;
            IdPuntoDue = idPuntoDue;
            Bidirezionale = bidirezionale;
            Abilitata = abilitata;
        }

        public int Id { get; set; }
        public int IdPuntoUno { get; set; }
        public int IdPuntoDue { get; set; }
        public bool Bidirezionale { get; set; }
        public bool Abilitata { get; set; }

    }
}
