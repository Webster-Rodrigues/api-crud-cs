using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APICatalago.Filters;

public class ApiExceptionFilter: IExceptionFilter
{
    private readonly ILogger<ApiExceptionFilter> logger;

    public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
    {
        this.logger = logger;
    }


    public void OnException(ExceptionContext context)
    {
        logger.LogError(context.Exception,"Ocorreu uma exceção não tratada");
        context.Result = new ObjectResult("Ocorreu um problema ao tratar a sua solicitação")
        {
            StatusCode = StatusCodes.Status500InternalServerError,
        };
    }
}