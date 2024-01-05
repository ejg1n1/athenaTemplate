namespace Application.Models.REST.Response;

public class UserResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;

    public string EmailAddress { get; set; } = String.Empty;
    public string FullName { get; set; } = String.Empty;

    public List<string>? Roles { get; set; }
}