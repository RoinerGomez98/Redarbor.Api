using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Redarbor.Api.Domain.Utils;
using System.Net;

namespace Redarbor.Api.Decorators
{
    public class FiltersAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<FiltersAttribute> logger;
        public FiltersAttribute(ILogger<FiltersAttribute> logger)
        {
            this.logger = logger;
        }
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            Handle(context);
            return Task.CompletedTask;
        }
        public override void OnException(ExceptionContext context)
        {
            Handle(context);
        }
        private void Handle(ExceptionContext context)
        {
            string exception = context.Exception.Message;

            logger.LogError(context.Exception, exception);

            ObjectResult result = new(new GenericResponse<string>() { Message = "Ha ocurrido un error interno.", Status = HttpStatusCode.InternalServerError })
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
            context.Result = result;
        }
    }
}
