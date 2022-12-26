using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ITI.LibrarySys.Presentation.Models
{
    public class AuthorModel
    {
        [Display(Name = "Author ID")]
        public int Id { get; set; }
        [Required(ErrorMessage = "This Field is Required"),
            MaxLength(200, ErrorMessage = "Minimum 5 Characters"),
            Display(Name = "Author Name")]
        public string Name { get; set; }
    }
}
