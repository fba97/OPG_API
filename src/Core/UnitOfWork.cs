using System;
using System.ComponentModel;
using System.Data;
using System.Reflection.Metadata;
using System.Resources;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Base
{
    public static class UnitOfWorkExtensions
    {
        //https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection-guidelines#disposable-transient-services-captured-by-container
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
         => services.AddSingleton<IUnitOfWorkFactory, UnitOfWorkFactory>();

        public static IUnitOfWork GetUnitOfWork(this IServiceProvider services)
         => services.GetRequiredService<IUnitOfWorkFactory>()
                    .Create();
    }
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly string _connString;

        public UnitOfWorkFactory(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("sqlStringConnection");
        }

        public IUnitOfWork Create()
            => new UnitOfWork(_connString);
    }


    public class UnitOfWork : IUnitOfWork
    {
        private readonly string _connString;
        private SqlConnection? _conn;
        private SqlTransaction? _trans;

        public UnitOfWork(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("sqlStringConnection");
            //PersonaggioRepository = new PersonaggioRepository(this);
            //PartitaRepository = new PartitaRepository(this);

        }

        public UnitOfWork(string connString)
        {
            _connString = connString;


            //PersonaggioRepository = new PersonaggioRepository(this);

            //PartitaRepository = new PartitaRepository(this);
        }

        //public IPersonaggioRepository PersonaggioRepository { get; }
        //public IPartitaRepository PartitaRepository { get; }    

        public SqlCommand GetCommand()
        {
            if (_conn is null)
                _conn = new SqlConnection(_connString);

            if (_conn.State != ConnectionState.Open)
                _conn.Open();

            if (_trans is null)
                _trans = _conn.BeginTransaction(nameof(UnitOfWork));

            var cmd = _conn.CreateCommand();

            cmd.Transaction = _trans;

            return cmd;
        }

        public async Task<SqlCommand> GetCommandAsync(CancellationToken token = default)
        {
            if (_conn is null)
                _conn = new SqlConnection(_connString);

            if (_conn.State != ConnectionState.Open)
                await _conn.OpenAsync(token);

            if (_trans is null)
                _trans = _conn.BeginTransaction(nameof(UnitOfWork));

            var cmd = _conn.CreateCommand();

            cmd.Transaction = _trans;

            return cmd;
        }

        public void SaveChanges()
        {
            if (_trans is null)
                throw new InvalidOperationException("No Changes to save, Transaction was null");

            _trans.Commit();
            _trans = null;
        }

        public async Task SaveChangesAsync(CancellationToken token = default)
        {
            if (_trans is null)
                throw new InvalidOperationException("No Changes to save, Transaction was null");

            await _trans.CommitAsync(token);

            _trans = null;
        }

        //https://learn.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-disposeasync#implement-both-dispose-and-async-dispose-patterns
        public void Dispose()
        {
            Dispose(disposing: true);

            GC.SuppressFinalize(this);
        }
        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                _trans?.Dispose();
                _conn?.Dispose();
            }

            _trans = null;
            _conn = null;
        }
        public async ValueTask DisposeAsync()
        {
            await DisposeAsyncCore().ConfigureAwait(false);

            Dispose(disposing: false);
            GC.SuppressFinalize(this);
        }
        protected virtual async ValueTask DisposeAsyncCore()
        {
            if (_trans is not null)
            {
                await _trans.DisposeAsync().ConfigureAwait(false);
            }
            if (_conn is not null)
            {
                await _conn.DisposeAsync().ConfigureAwait(false);
            }

            _conn = null;
            _trans = null;
        }

        ~UnitOfWork()
        {
            Dispose(disposing: false);
        }
    }
}