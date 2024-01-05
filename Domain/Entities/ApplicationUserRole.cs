using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities;

public class ApplicationUserRole: IdentityUserRole<Guid>
{
    public override Guid UserId { get; set; }
    public override Guid RoleId { get; set; }
    [ForeignKey("UserId")]
    public virtual ApplicationUser User { get; set; } = null!;

    [ForeignKey("RoleId")]
    public virtual ApplicationRole Role { get; set; } = null!;
}