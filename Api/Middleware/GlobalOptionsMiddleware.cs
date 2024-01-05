using System.Security.Claims;
using Application.Interfaces;

namespace Api.Middleware;

public class GlobalOptionsMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalOptionsMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IGlobalConstants globalConstantsService)
    {
        globalConstantsService.CurrentUserRoles = context.User.Claims
            .Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value)
            .ToList();
            
        Guid.TryParse(context.User.FindFirstValue(ClaimTypes.NameIdentifier), out var currentUserId);

        globalConstantsService.CurrentUserId = currentUserId;

        await _next(context);
    }
}