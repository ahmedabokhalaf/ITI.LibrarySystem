using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ITI.LibrarySys.Presentation.Models
{
    public class ChangePasswordModel
    {        
        [Required, MinLength(8), MaxLength(32),
            Display(Name = "Current Password"),
            DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        [Required, MinLength(8), MaxLength(32),
            Display(Name = "New Password"),
            DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Compare("NewPassword"), Display(Name = "Confirm New Password")
            , DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
        
    }
}
