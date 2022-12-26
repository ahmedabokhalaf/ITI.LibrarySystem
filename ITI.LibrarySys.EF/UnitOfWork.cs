using ITI.LibrarySys.Core;
using ITI.LibrarySys.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.LibrarySys.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private Context context;
        public IAuthorRepo Authors { get; private set; }

        public IBookRepo Books { get; private set; }
        public UnitOfWork(Context _context)
        {
            context = _context;
            Authors = new AuthorRepo(context);
            Books = new BookRepo(context);
        }
        public void Dispose()
        {
            context.Dispose();
        }

        public int Save()
        {
            return context.SaveChanges();
        }
    }
}
