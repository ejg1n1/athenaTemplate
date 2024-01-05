namespace Application.Models.REST.Response;

public class AuthenticateResponse
{
    public AuthenticateResponse()
    {
        Roles = new List<string>();
    }

    public Guid Id { get; set; }
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public string UserName { get; set; } = String.Empty;
    public string Token { get; set; } = String.Empty;

    public List<string> Roles { get; set; }
}