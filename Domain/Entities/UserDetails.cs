using Athena.Core.Entities;

namespace Core.Entities;

public class UserDetails : BaseEntity
{
    public string Details { get; set; } = String.Empty;
    
    public virtual ApplicationUser? DetailApplicationUser { get; set; }
}