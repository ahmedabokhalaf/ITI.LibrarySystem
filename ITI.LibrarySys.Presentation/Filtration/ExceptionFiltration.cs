using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.LibrarySys.Presentation.Filtration
{
    public class ExceptionFiltration : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            string msg = $"Date-Time: {DateTime.Now}\n Description: {context.Exception.Message}";
            File.WriteAllText("Audit.txt", msg);
            context.Result = new ViewResult()
            {
                ViewName = "Error"
            };
            base.OnException(context);
        }
    }
}
