using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Threading;
using Microsoft.Data.SqlClient;
using Core.Repository;

namespace Core.Base
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }

    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        void SaveChanges();

        Task SaveChangesAsync(CancellationToken token = default);

        SqlCommand GetCommand();

        Task<SqlCommand> GetCommandAsync(CancellationToken token = default);

        //IPersonaggioRepository PersonaggioRepository { get; }
        //IPartitaRepository PartitaRepository { get; }

    }
}
