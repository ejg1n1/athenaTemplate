using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities;

public class ApplicationUserLogin: IdentityUserLogin<Guid>
{
    [ForeignKey("UserId")] public virtual ApplicationUser User { get; set; } = null!;
}