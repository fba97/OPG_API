using System;
using System.Collections.Concurrent;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection.Metadata.Ecma335;

namespace Core
{
    public static class Mappa
    {
        //private readonly Area[] _aree;
        //private readonly SottoArea[] _sottoAree;
        //private readonly Componente[] _componenti;
        //private readonly Partizione[] _partizioni;
        //private readonly IEnumerable<PersonaggioInPartita> _eroiInPartita;
        //private readonly IEnumerable<PersonaggioInPartita> _nemiciInPartita;
        //private readonly IServiceProvider _services;
        //private readonly DataAccess _dataAccess;
        //private int _count = 0;

        //puo essere che qui come in plant ci possa essere bisogno dei lock per evitare di andare a scrivere piu cose allo stesso momento. bisogna apire cosa vuole evitare coi lock e se io ho la stessa situazione spiacevole.
        // i lock servono per evitare di prendere con due thread la stessa risorsa. e quidni se passo Game come classe statica in giro è possibile che uso la programmazione concorrente magari per prendere da database e aggiornare .

        //public Mappa(IServiceProvider services, DataAccess dataAccess)
        //{
        //    _services = services;
        //    _dataAccess = dataAccess;


        //    //_aree = aree;
        //    //_sottoAree = sottoAree;
        //    //_componenti = componenti;
        //    //_partizioni = partizioni;
        //    //_eroiInPartita = dataAccess.GetAllPersonaggiInPartitaFromDb(); //serve il metodo non async perchè sono in una classe non asincrona. la cosa da capire è... devo creare un metodo del data access sincrono oppure modifico questa classe o c'è altro?
        //    //_nemiciInPartita = dataAccess.GetAllNpcsFromDb();
        //}
        public static int Counto { get { return 0; } }

        //public IEnumerable<Area> Aree { get => _aree; }
        //public IEnumerable<SottoArea> SottoAree { get => _sottoAree; }
        //public IEnumerable<Componente> Componenti { get => _componenti; }
        //public IEnumerable<Partizione> Partitions { get => _partizioni; }
        //public IEnumerable<PersonaggioInPartita> EroiInPartita { get => _eroiInPartita; }
        //public IEnumerable<PersonaggioInPartita> NemiciInPartita { get => _nemiciInPartita; }

    }
}
