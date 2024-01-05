using Serilog;

namespace Api.Middleware;

public class CustomExceptionLoggerMiddleware
{
    private readonly RequestDelegate _next;

    public CustomExceptionLoggerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            var routeData = httpContext.GetRouteData();
            var actionName = routeData.Values["action"]?.ToString();
            Log.Error(ex, $" Method: {httpContext.Request.Method} " +
                          $"-- Action:{httpContext.GetRouteValue("controller")}: {actionName} " +
                          $"--Exception: {ex.Message}");
            throw;
        }
    }
}