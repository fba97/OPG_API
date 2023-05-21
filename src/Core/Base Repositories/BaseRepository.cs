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
    public abstract class BaseRepository<T> : BaseReadonlyRepository<T>, IRepository<T>, IAsyncRepository<T>
        where T : class,IRepositoryItem
    {
        protected abstract string AddElement { get; }
        protected abstract string UpdateElementById { get; }
        protected abstract string DeleteElementById { get; }

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

        public virtual T Update(T element)
        {
            var parameters = GetParameters(element);

            using var cmd = UnitOfWork.GetCommand();
            cmd.CommandText = UpdateElementById;
            cmd.Parameters.AddRange(parameters);

            cmd.ExecuteNonQuery();

            return GetElement(cmd.Parameters);
        }

        public virtual void Delete(int id)
        {
            using var cmd = UnitOfWork.GetCommand();
            cmd.CommandText = DeleteElementById;
            cmd.Parameters.AddWithValue(_id, id);

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

        public virtual async Task<T> UpdateAsync(T element, CancellationToken token = default)
        {
            var parameters = GetParameters(element);

            await using var cmd = await UnitOfWork.GetCommandAsync(token);
            cmd.CommandText = UpdateElementById;
            cmd.Parameters.AddRange(parameters);

            await cmd.ExecuteNonQueryAsync(token);

            return GetElement(cmd.Parameters);
        }

        public virtual async Task DeleteAsync(int id, CancellationToken token = default)
        {
            await using var cmd = await UnitOfWork.GetCommandAsync(token);
            cmd.CommandText = DeleteElementById;
            cmd.Parameters.AddWithValue(_id, id);

            await cmd.ExecuteNonQueryAsync(token);

        }
    }
}
