using Athena.Core.Entities;

namespace Core.Entities;

public class Post : BaseEntity
{
    public decimal Price { get; set; }
    public string Description { get; set; } = String.Empty;
    public Guid? PostStatusId { get; set; }
    public virtual PostStatus? PostStatus { get; set; }
    public virtual ApplicationUser? PostOwner { get; set; }
    public virtual IList<Photos>? PostImages { get; set; }
}