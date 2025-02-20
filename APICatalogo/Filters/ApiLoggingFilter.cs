using Microsoft.AspNetCore.Mvc.Filters;

namespace APICatalogo.Filters;

public class ApiLoggingFilter : IActionFilter
{
    public readonly ILogger<ApiLoggingFilter> logger;

    public ApiLoggingFilter(ILogger<ApiLoggingFilter> logger)
    {
        this.logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context) 
    {
        var method = context.HttpContext.Request.Method; 
        var path = context.HttpContext.Request.Path; 
        var parameters = string.Join(", ", context.ActionArguments.Select(a => $"{a.Key}: {a.Value}"));

        logger.LogInformation("### Iniciando -> {Method} {Path} ({Parameters})", method, path, parameters);
        logger.LogInformation("ModelState válido: {ModelState}", context.ModelState.IsValid);
    }

    public void OnActionExecuted(ActionExecutedContext context) 
    {
        var method = context.HttpContext.Request.Method;
        var path = context.HttpContext.Request.Path;
        var statusCode = context.HttpContext.Response.StatusCode;

        if (context.Exception == null) {
            logger.LogInformation("Concluído -> {Method} {Path} | Status Code: {StatusCode}", 
                method, path, statusCode);
        }
        else {
            logger.LogError(context.Exception, "Erro na execução -> {Method} {Path}", method, path);
        }
    }

}

//Classe utilizada para exec ações específicas ANTES e DEPOIS de uma Action