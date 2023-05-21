using Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Controllers
{
    public class PersonaggiController
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly DataAccess _dataAccess;
        //get getAll add 
        public PersonaggiController(IServiceProvider serviceProvider, DataAccess dataAccess)
        {
            _serviceProvider = serviceProvider;
            _dataAccess = dataAccess;
        }

        public async Task<IEnumerable<PersonaggioInPartita>> AddPersonaggiAllaPartita(IEnumerable<int> listaIdPersonaggi, int idPartita)
        {
            IEnumerable<PersonaggioBase> personaggiBase = await _dataAccess.GetAllPersonaggiBaseFromDb();
            var personaggiBasePerId = personaggiBase.Where(pb => listaIdPersonaggi.Contains(pb.Id));
            //aggiungi i pg al databese
            foreach(var pg in personaggiBasePerId) 
            {
                await _dataAccess.AddPersonaggioInPartotaToDb(pg, idPartita);
            }
            return await _dataAccess.GetAllPersonaggiInPartitaFromDb();
        }

    }
}
