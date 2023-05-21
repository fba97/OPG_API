using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Base
{ 
    public interface IGenericRepository<T>
    {
        IEnumerable<T> Get();
    }

    public interface IReadOnlyRepository<T> : IGenericRepository<T>
        where T : IRepositoryItem
    {
        T? Get(int id);
    }

    public interface IReadOnlyUniqueCodeRepository<T> : IGenericRepository<T>
        where T : IUniqueCodeRepositoryItem
    {
        T? Get(string code);
    }

    public interface ICompleteReadOnlyRepository<T> : IReadOnlyUniqueCodeRepository<T>, IReadOnlyRepository<T>
        where T : IUniqueCodeRepositoryItem, IRepositoryItem
    {

    }

    public interface IEditableRepository<T>
    {
        T Add(T element);

        T Update(T element);
    }

    public interface IRepository<T> : IReadOnlyRepository<T> , IEditableRepository<T>
        where T : IRepositoryItem
    {
        void Delete(int id);
    }

    public interface IUniqueCodeRepository<T> : IReadOnlyUniqueCodeRepository<T>, IEditableRepository<T>
        where T : IUniqueCodeRepositoryItem
    {
        void Delete(string code);
    }

    public interface ICompleteRepository<T> : IUniqueCodeRepository<T>, IRepository<T>
        where T : IUniqueCodeRepositoryItem, IRepositoryItem
    {

    }
}
