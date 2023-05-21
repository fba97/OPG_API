using Primitives;
using System.Runtime.CompilerServices;

namespace Core.Controllers
{
    public class PartitaController
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly DataAccess _dataAccess;
        //get getAll add 
        public PartitaController(IServiceProvider serviceProvider, DataAccess dataAccess) 
        {
            _serviceProvider = serviceProvider;
            _dataAccess = dataAccess;
        }  
        public Partita CreaPartita(IEnumerable<int> personaggiInGioco, string nome)
        {
            //controlli vari sulla possibilità di aggiungere la partita
            //se ha lo stesso nome di altri non si puo aggiungere
            var partita = new Partita
            {
                PersonaggiInGioco = personaggiInGioco,
                Nome = nome,
                DataInizioPartita = DateTime.Now,
                StatoPartita = 1 //nuova
            };

            //creazione dei personaggi in partita
            
            //_dataAccess.

            return partita;
        }

        public Partita LoadPartita(int idPartita){ return new Partita(); }

        }
}