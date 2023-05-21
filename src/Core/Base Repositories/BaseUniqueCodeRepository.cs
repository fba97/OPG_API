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
    public abstract class BaseUniqueCodeRepository<T> 
        : 
            BaseReadonlyUniqueCodeRepository<T>,
            IUniqueCodeRepository<T>,
            IUniqueCodeAsyncRepository<T>
        
        where T : class,IUniqueCodeRepositoryItem
    {
        protected abstract string AddElement { get; }
        protected abstract string UpdateElementByCode { get; }
        protected abstract string DeleteElementByCode { get; }

        protected abstract SqlParameter[] GetParameters(T element);
        protected abstract T GetElement(SqlParameterCollection collection);

        public T Add(T element)
        {
            var parameters = GetParameters(element);

            using var cmd = UnitOfWork.GetCommand();
            cmd.CommandText = AddElement;
            cmd.Parameters.AddRange(parameters);

            cmd.ExecuteNonQuery();

            return GetElement(cmd.Parameters);
        }

        public T Update(T element)
        {
            var parameters = GetParameters(element);

            using var cmd = UnitOfWork.GetCommand();
            cmd.CommandText = UpdateElementByCode;
            cmd.Parameters.AddRange(parameters);

            cmd.ExecuteNonQuery();

            return element;
        }

        public void Delete(string code)
        {
            using var cmd = UnitOfWork.GetCommand();
            cmd.CommandText = DeleteElementByCode;
            cmd.Parameters.AddWithValue(_code, code);

            cmd.ExecuteNonQuery();
        }

        public async Task<T> AddAsync(T element, CancellationToken token = default)
        {
            var parameters = GetParameters(element);

            await using var cmd = await UnitOfWork.GetCommandAsync(token);
            cmd.CommandText = AddElement;
            cmd.Parameters.AddRange(parameters);

            await cmd.ExecuteNonQueryAsync(token);

            return GetElement(cmd.Parameters);
        }

        public async Task<T> UpdateAsync(T element, CancellationToken token = default)
        {
            var parameters = GetParameters(element);

            await using var cmd = await UnitOfWork.GetCommandAsync(token);
            cmd.CommandText = UpdateElementByCode;
            cmd.Parameters.AddRange(parameters);

            await cmd.ExecuteNonQueryAsync(token);

            return GetElement(cmd.Parameters);
        }

        public async Task DeleteAsync(string code, CancellationToken token = default)
        {
            await using var cmd = await UnitOfWork.GetCommandAsync(token);
            cmd.CommandText = DeleteElementByCode;
            cmd.Parameters.AddWithValue(_code, code);

            await cmd.ExecuteNonQueryAsync(token);
        }
    }
}
