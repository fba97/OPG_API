using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Core.Base_Repositories
{
    public abstract class BaseCompleteRepository<T> 
            :   
                BaseRepository<T>, 
                ICompleteRepository<T>, 
                ICompleteAsyncRepository<T>

        where T 
            :   
                class, 
                IRepositoryItem, 
                IUniqueCodeRepositoryItem
    {
        protected abstract string GetElementByCode { get; }
        protected abstract string DeleteElementByCode { get; }
        protected abstract string UpdateElementByCode { get; }

        public void Delete(string code)
        {
            using var cmd = UnitOfWork.GetCommand();
            cmd.CommandText = DeleteElementByCode;
            cmd.Parameters.AddWithValue(_code, code);

            cmd.ExecuteNonQuery();
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

        public override T Update(T element)
        {
            if (element.Id == 0)
                return UpdateByCode(element);
            else 
                return base.Update(element);
        }
        
        private T UpdateByCode(T element)
        {
            var parameters = GetParameters(element);

            using var cmd = UnitOfWork.GetCommand();
            cmd.CommandText = UpdateElementByCode;
            cmd.Parameters.AddRange(parameters);

            cmd.ExecuteNonQuery();
            return GetElement(cmd.Parameters);
        }

        public async Task DeleteAsync(string code, CancellationToken token = default)
        {
            await using var cmd = await UnitOfWork.GetCommandAsync(token);
            cmd.CommandText = DeleteElementByCode;
            cmd.Parameters.AddWithValue(_code, code);

            await cmd.ExecuteNonQueryAsync(token);
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

        public override async Task<T> UpdateAsync(T element, CancellationToken token = default)
        {
            if (element.Id == 0)
                return await UpdateByCodeAsync(element, token);
            else
                return await base.UpdateAsync(element, token);
        }

        private async Task<T> UpdateByCodeAsync(T element, CancellationToken token)
        {
            var parameters = GetParameters(element);

            using var cmd = UnitOfWork.GetCommand();
            cmd.CommandText = UpdateElementByCode;
            cmd.Parameters.AddRange(parameters);

            await cmd.ExecuteNonQueryAsync(token);
            return GetElement(cmd.Parameters);
        }

        protected const string _code = "@Code";
    }
}
