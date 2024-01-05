using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<
    ApplicationUser,
    ApplicationRole,
    Guid,
    ApplicationUserClaim,
    ApplicationUserRole,
    ApplicationUserLogin, 
    ApplicationRoleClaim,
    ApplicationUserToken>
{
    public DbSet<Product> Products { get; set; } = default!;
    public DbSet<UserDetails> UserDetails { get; set; } = default!;

    public DbSet<Address> Addresses { get; set; } = default!;
    public DbSet<AddressStatus> AddressStatuses => Set<AddressStatus>();

    public DbSet<Photos> Photos { get; set; } = default!;
    
    public DbSet<Post> Posts { get; set; } = default!;
    public DbSet<PostStatus> PostStatuses => Set<PostStatus>();
    


    public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
    }
}
