using System.ComponentModel.DataAnnotations;
using Core.Attributes;

namespace Application.Models.REST.Request;

public class PostStatusRequest
{
    [Required(AllowEmptyStrings = false)]
    [PatchDisabled]
    public string Name { get; set; } = String.Empty;
}