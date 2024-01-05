namespace Application.Interfaces;

public interface IGlobalConstants
{
    public Guid CurrentUserId { get; set; }
    public List<string> CurrentUserRoles { get; set; }
}