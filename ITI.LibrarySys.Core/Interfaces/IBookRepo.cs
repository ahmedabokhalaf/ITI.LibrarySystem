using ITI.LibrarySys.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.LibrarySys.Core.Interfaces
{
    public interface IBookRepo : IBaseRepo<Book>
    {
        void Add(Book book);
        void Update(Book book);
    }
}
