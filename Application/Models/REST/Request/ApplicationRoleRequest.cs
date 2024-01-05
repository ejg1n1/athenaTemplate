using Core.Attributes;

namespace Application.Models.REST.Request;

public class ApplicationRoleRequest
{
    [PatchDisabled] public string Name { get; set; } = String.Empty;
}