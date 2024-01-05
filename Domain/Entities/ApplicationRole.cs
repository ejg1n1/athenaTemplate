using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities;

public class ApplicationRole: IdentityRole<Guid>
{
    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; } = new List<ApplicationUserRole>();
    public virtual ICollection<ApplicationRoleClaim> RoleClaims { get; set; } = new List<ApplicationRoleClaim>();
}