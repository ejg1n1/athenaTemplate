using Application.Interfaces;

namespace Application.Services;

public class GlobalConstantsService : IGlobalConstants
{
    public List<string> CurrentUserRoles { get; set; } = new List<string>();
    public Guid CurrentUserId { get; set; }
}