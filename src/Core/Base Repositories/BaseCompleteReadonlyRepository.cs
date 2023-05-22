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
    public abstract class BaseCompleteReadonlyRepository<T>
        :
            BaseReadonlyRepository<T>,
            ICompleteReadOnlyRepository<T>,
            ICompleteReadOnlyAsyncRepository<T>
        where T : class, IRepositoryItem, IUniqueCodeRepositoryItem
    {
        protected abstract string GetElementByCode { get; }

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

        protected const string _code = "@Code";
    }
}
