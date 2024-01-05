using System.ComponentModel.DataAnnotations;

namespace Application.Models.REST.Request;

public class UserRequest
{
    [Required(AllowEmptyStrings = false)] public string FirstName { get; set; } = string.Empty;
    [Required(AllowEmptyStrings = false)] public string LastName { get; set; } = string.Empty;
    [Required] [EmailAddress] public string EmailAddress { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;
    
    [Required] public string Password { get; set; } = string.Empty;
    public List<string>? Roles { get; set; }
}