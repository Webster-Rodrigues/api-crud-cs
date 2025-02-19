using Microsoft.AspNetCore.Mvc.Filters;

namespace APICatalago.Filters;

public class ApiLoggingFilter : IActionFilter
{
    public readonly ILogger<ApiLoggingFilter> logger;

    public ApiLoggingFilter(ILogger<ApiLoggingFilter> logger)
    {
        this.logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context) 
    {
        //Antes da Action
        logger.LogInformation("### Executando -> OnActionExecuting");
        logger.LogInformation("############################################################");
        logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
        logger.LogInformation($"ModelState {context.ModelState.IsValid}");
        logger.LogInformation("############################################################");
    }

    public void OnActionExecuted(ActionExecutedContext context) 
    {
        //Depois da Action
        logger.LogInformation("### Executando -> OnActionExecuted");
        logger.LogInformation("############################################################");
        logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
        logger.LogInformation($"Status Code {context.HttpContext.Response.StatusCode}");
        logger.LogInformation("############################################################");
    }
}

//Classe utilizada para exec ações específicas ANTES e DEPOIS de uma Action