using System.Runtime.CompilerServices;
using Core.Base;
using Core.Game;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Controllers
{
    public class PartitaController
    {
        private readonly IServiceProvider _serviceProvider;

        //public PartitaController(IServiceProvider serviceProvider)
        //{
        //    _serviceProvider = serviceProvider;
        //}
        //public Partita CreaPartita(IEnumerable<int> personaggiInGioco, string nome)
        //{
        //    //controlli vari sulla possibilità di aggiungere la partita
        //    //se ha lo stesso nome di altri non si puo aggiungere
        //    var partita = new Partita
        //    {
        //        PersonaggiInGioco = personaggiInGioco,
        //        Nome = nome,
        //        DataInizioPartita = DateTime.Now,
        //        StatoPartita = 1 //nuova
        //    };

        //    //creazione dei personaggi in partita

        //    //_dataAccess.

        //    return partita;
        //}



    }
}