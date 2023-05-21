using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core
{
    public interface IRepositoryItem
    {
        int Id { get; }
    }

    public interface IUniqueCodeRepositoryItem 
    {
        string Code { get; }
    }
}
