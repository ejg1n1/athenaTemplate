using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    [Required]
    public string FirstName { get; set; } = String.Empty;
    [Required]
    public string LastName { get; set; } = String.Empty;
    [NotMapped]
    public string FullName => this.FirstName + " " + this.LastName;
    public virtual IList<Address> UserAddresses { get; set; } = new List<Address>();
    public virtual ICollection<ApplicationUserClaim> Claims { get; set; } = new List<ApplicationUserClaim>();
    public virtual ICollection<ApplicationUserLogin> Logins { get; set; } = new List<ApplicationUserLogin>();
    public virtual ICollection<ApplicationUserToken> Tokens { get; set; } = new List<ApplicationUserToken>();
    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; } = new List<ApplicationUserRole>();
}