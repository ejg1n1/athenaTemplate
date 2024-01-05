using Athena.Core.Entities;

namespace Core.Entities;

public class Address : BaseEntity
{
    public string AddressLine1 { get; set; } = String.Empty;
    public string AddressLine2 { get; set; } = String.Empty;
    public string Suburb { get; set; } = String.Empty;
    public string City { get; set; } = String.Empty;
    public string Country { get; set; } = String.Empty;
    public string PostalCode { get; set; } = String.Empty;

    public Guid? UserId { get; set; }
    public virtual ApplicationUser? User { get; set; }
}