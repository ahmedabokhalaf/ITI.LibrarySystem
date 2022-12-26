using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.LibrarySys.Presentation.Filtration
{
    public class LogFiltration : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string logData = $"\nDate-Time: {DateTime.Now}\nRequestPath: {context.HttpContext.Request.Path}" +
                $"\nStatus: Before Executing\nUser Name: {context.HttpContext.User.Identity.Name}" +
                $"\n-------------------------------------------------------------------------";
            File.AppendAllText("log.txt", logData);
            base.OnActionExecuting(context);
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            string logData = $"\nDate-Time: {DateTime.Now}\nRequestPath: {context.HttpContext.Request.Path}" +
                $"\nStatus: After Executing\nUser Name: {context.HttpContext.User.Identity.Name}" +
                $"\n-------------------------------------------------------------------------";
            File.AppendAllText("log.txt", logData);
            base.OnActionExecuted(context);
        }
    }
}
