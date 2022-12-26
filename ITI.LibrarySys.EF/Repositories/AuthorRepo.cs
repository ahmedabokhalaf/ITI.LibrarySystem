using ITI.LibrarySys.Core.Interfaces;
using ITI.LibrarySys.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.LibrarySys.EF
{
    public class AuthorRepo : BaseRepo<Author>, IAuthorRepo
    {
        private Context context;

        public AuthorRepo(Context _context) : base(_context)
        {
        }

        public void Add(Author author)
        {
            context.Authors.Add(author);
        }
        public void Update(Author author)
        {
            context.Authors.Update(author);
        }
    }
}
