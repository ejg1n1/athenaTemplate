using System.ComponentModel.DataAnnotations;
using Athena.Core.Entities;

namespace Core.Entities;

public class PostStatus : BaseEntity
{
    [Required]
    public string Description { get; set; } = String.Empty;
}