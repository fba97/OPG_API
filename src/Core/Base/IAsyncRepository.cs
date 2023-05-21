using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Base
{
    public interface IGenericAsyncRepository<T>
    {
        Task<IEnumerable<T>> GetAsync(CancellationToken token = default);
    }

    public interface IReadOnlyAsyncRepository<T> : IGenericAsyncRepository<T>
        where T : IRepositoryItem
    {
        Task<T?> GetAsync(int id, CancellationToken token = default);
    }

    public interface IReadOnlyUniqueCodeAsyncRepository<T> : IGenericAsyncRepository<T>
        where T : IUniqueCodeRepositoryItem
    {
        Task<T?> GetAsync(string code, CancellationToken token = default);
    }

    public interface ICompleteReadOnlyAsyncRepository<T> : IReadOnlyUniqueCodeAsyncRepository<T>, IReadOnlyAsyncRepository<T>
        where T : IUniqueCodeRepositoryItem, IRepositoryItem
    {

    }

    public interface IEditableAsyncRepository<T> 
    {
        Task<T> AddAsync(T element, CancellationToken token = default);

        Task<T> UpdateAsync(T element, CancellationToken token = default);
    }

    public interface IAsyncRepository<T> : IReadOnlyAsyncRepository<T>, IEditableAsyncRepository<T>
        where T : IRepositoryItem
    {
        Task DeleteAsync(int id, CancellationToken token = default);
    }    

    public interface IUniqueCodeAsyncRepository<T> : IReadOnlyUniqueCodeAsyncRepository<T>, IEditableAsyncRepository<T>
        where T : IUniqueCodeRepositoryItem
    {
        Task DeleteAsync(string code, CancellationToken token = default);
    }

    public interface ICompleteAsyncRepository<T> : IUniqueCodeAsyncRepository<T>, IAsyncRepository<T>
        where T : IUniqueCodeRepositoryItem, IRepositoryItem
    {

    }
}
