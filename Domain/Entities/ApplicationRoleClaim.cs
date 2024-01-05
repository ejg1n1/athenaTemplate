using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities;

public class ApplicationRoleClaim: IdentityRoleClaim<Guid>
{
    [ForeignKey("RoleId")] public virtual ApplicationRole Role { get; set; } = null!;
}