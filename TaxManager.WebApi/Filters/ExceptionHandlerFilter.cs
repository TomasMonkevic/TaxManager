using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using TaxManager.Service.Exceptions;

namespace TaxManager.WebApi.Filters {
    
    public class ExceptionHandlerFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is TaxNotAppliedException) 
            {
                context.Result = new NotFoundResult();
            }
            else 
            {
                context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            context.ExceptionHandled = true;
        }
    }
}