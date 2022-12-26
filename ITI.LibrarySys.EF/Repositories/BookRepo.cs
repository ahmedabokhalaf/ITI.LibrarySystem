using ITI.LibrarySys.Core.Interfaces;
using ITI.LibrarySys.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.LibrarySys.EF
{
    public class BookRepo : BaseRepo<Book>, IBookRepo
    {
        private Context context;

        public BookRepo(Context _context) : base(_context)
        {
        }

        public void Add(Book book)
        {
            context.Books.Add(book);
        }

        public void Update(Book book)
        {
            context.Books.Update(book);
        }
    }
}
