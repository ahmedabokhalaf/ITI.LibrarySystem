using ITI.LibrarySys.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.LibrarySys.EF
{
    public class BaseRepo<T> : IBaseRepo<T> where T : class
    {
        private Context context;
        public BaseRepo(Context _context)
        {
            context = _context;
        }

        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return context.Set<T>().Find(id);
        }
    }
}
