using Core.Base;
using Core.Primitives;
using Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core
{
    public interface IPartitaRepository 
        : 
            ICompleteRepository<Partita>,
            ICompleteAsyncRepository<Partita>
    {
        PersonaggioInPartita? GetById(int id);

        Task<Partita?> GetByIdAsync(int id, CancellationToken token = default);

     }
}
