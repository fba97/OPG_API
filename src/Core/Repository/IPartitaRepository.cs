using Core.Base;
using Core.Primitives;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IPartitaRepository 
    {
        Task<Partita?> GetByIdAsync(int id, CancellationToken token = default);
        Task<Partita?> AddAsync(Partita partita, CancellationToken token = default);
        Task<Partita?> UpdateAsync(Partita partita, CancellationToken token = default);
        Task<int> DeleteAsync(int id, CancellationToken token = default);
        Task<Partita?> AddOrUpdatePartitaAsync(Partita partitaToAddOrUpdate, CancellationToken token = default);
        Task<IEnumerable<Partita>> GetAllAsync(CancellationToken token = default);

        Partita? GetById(int id);
        Partita? Add(Partita partita);
        Partita? Update(Partita partita);
        Partita? AddOrUpdatePartita(Partita partitaToAddOrUpdate);
        int Delete(int id);
    }
}
