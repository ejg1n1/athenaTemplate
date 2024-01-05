using System.ComponentModel.DataAnnotations;

namespace Application.Models.REST.Request;

public class AuthenticateRequest
{
    [Required(AllowEmptyStrings = false)] public string Username { get; set; } = String.Empty;

    [Required(AllowEmptyStrings = false)] public string Password { get; set; } = String.Empty;
}