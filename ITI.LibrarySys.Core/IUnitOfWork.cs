using ITI.LibrarySys.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.LibrarySys.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IAuthorRepo Authors { get; }
        IBookRepo Books { get; }
        int Save();
    }
}
