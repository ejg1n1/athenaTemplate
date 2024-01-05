using System.ComponentModel.DataAnnotations;
using Athena.Core.Entities;

namespace Core.Entities;

public class AddressStatus : BaseEntity
{
    [Required] public string Description { get; set; } = String.Empty;
}