using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primitives
{
    public class Turno
    {


        public Turno(List<int> personaggiIds, int idDelPersonaggioInTurno, int turnoCorrente, bool giocoIniziato, int azioniMassimePerTurno)
        {
            PersonaggiIds = personaggiIds;
            IdDelPersonaggioInTurno = idDelPersonaggioInTurno;
            TurnoCorrente = turnoCorrente;
            GiocoIniziato = giocoIniziato;
            AzioniMassimePerTurno = azioniMassimePerTurno;
        }

        public Turno() { }

        public List<int> PersonaggiIds { get; set; } = new List<int>();
        public int IdDelPersonaggioInTurno { get; set; } = 0;
        public int TurnoCorrente { get; set; } = 0;
        public bool GiocoIniziato { get; set; } = false;
        public int AzioniMassimePerTurno { get; set; } = 2;
        public int AzioniRimanenti { get; set; } = 2;

        public static Turno StartGame(List<int> personaggiIds)
        {
            if (personaggiIds.Count() == 0)
                return new Turno();
            return new Turno()
            {
                PersonaggiIds = personaggiIds,
                GiocoIniziato = true,
                IdDelPersonaggioInTurno = personaggiIds.First()
            };
        }
    }
}
