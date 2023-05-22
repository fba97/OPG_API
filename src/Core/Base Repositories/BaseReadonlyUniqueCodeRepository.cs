using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Base;
using Microsoft.Data.SqlClient;

namespace Core.Base_Repositories
{
    public abstract class BaseReadonlyUniqueCodeRepository<T>
        :
            IReadOnlyUniqueCodeRepository<T>,
            IReadOnlyUniqueCodeAsyncRepository<T>

        where T : class, IUniqueCodeRepositoryItem
    {
        protected abstract UnitOfWork UnitOfWork { get; }

        protected abstract string GetElementByCode { get; }
        protected abstract string GetElements { get; }

        protected abstract T ParseReader(SqlDataReader r);

        protected IEnumerable<T> ParseEnumerableReader(SqlDataReader r)
        {
            var result = new List<T>();
            while (r.Read())
            {
                var item = ParseReader(r);

                result.Add(item);
            }
            return result;
        }
        protected async Task<IEnumerable<T>> ParseEnumerableReaderAsync(SqlDataReader r, CancellationToken token = default)
        {
            var result = new List<T>();
            while (await r.ReadAsync(token))
            {
                var item = ParseReader(r);

                result.Add(item);
            }
            return result;
        }

        public T? Get(string code)
        {
            using var cmd = UnitOfWork.GetCommand();
            cmd.CommandText = GetElementByCode;
            cmd.Parameters.AddWithValue(_code, code);

            using var r = cmd.ExecuteReader();

            if (r.HasRows && r.Read())
                return ParseReader(r);
            else
                return null;
        }


        public IEnumerable<T> Get()
        {
            using var cmd = UnitOfWork.GetCommand();
            cmd.CommandText = GetElements;

            using var r = cmd.ExecuteReader();

            return ParseEnumerableReader(r);
        }

        public async Task<T?> GetAsync(string code, CancellationToken token = default)
        {
            await using var cmd = await UnitOfWork.GetCommandAsync(token);
            cmd.CommandText = GetElementByCode;
            cmd.Parameters.AddWithValue(_code, code);

            using var r = await cmd.ExecuteReaderAsync(token);

            if (r.HasRows && await r.ReadAsync(token))
                return ParseReader(r);
            else
                return null;
        }

        public async Task<IEnumerable<T>> GetAsync(CancellationToken token = default)
        {
            await using var cmd = await UnitOfWork.GetCommandAsync(token);
            cmd.CommandText = GetElements;

            await using var r = await cmd.ExecuteReaderAsync(token);

            return await ParseEnumerableReaderAsync(r, token);
        }

        protected const string _code = "@Code";
    }
}
