using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.LibrarySys.Core.Models
{
    public class BookImages
    {
        public int ID { get; set; }
        public int BookID { get; set; }
        public string Path { get; set; }
        public virtual Book Book { get; set; }
    }
}
