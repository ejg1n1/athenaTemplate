using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class ApplicationUserConfig: IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        //Primary Key
        builder.HasKey(u => u.Id);
        
        // Indexes for normalised username and email, allows efficient lookups
        builder.HasIndex(u => u.NormalizedUserName).HasDatabaseName("UserNameIndex").IsUnique();
        builder.HasIndex(u => u.NormalizedEmail).HasDatabaseName("EmailIndex").IsUnique();
        
        //Maps to table name
        builder.ToTable("AspNetUsers");
        
        // Concurrency token for use with Optimistic concurrency checking
        builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();
        
        //limit size of specific fields
        builder.Property(u => u.UserName).HasMaxLength(256);
        builder.Property(u => u.NormalizedUserName).HasMaxLength(256);
        builder.Property(u => u.Email).HasMaxLength(256);
        builder.Property(u => u.NormalizedEmail).HasMaxLength(256);
        
        // Each User can have many Addresses
        builder.HasMany(a => a.UserAddresses).WithOne(u => u.User).HasForeignKey(ua => ua.UserId).IsRequired();
        
        // Each User can have many UserClaims
        builder.HasMany(o => o.Claims).WithOne(o => o.User).HasForeignKey(uc => uc.UserId).IsRequired();

        // Each User can have many UserLogins
        builder.HasMany(o => o.Logins).WithOne(o => o.User).HasForeignKey(ul => ul.UserId).IsRequired();

        // Each User can have many UserTokens
        builder.HasMany(o => o.Tokens).WithOne(o => o.User).HasForeignKey(ut => ut.UserId).IsRequired();

        // Each User can have many entries in the UserRole join table
        builder.HasMany(o => o.UserRoles).WithOne(o => o.User).HasForeignKey(ur => ur.UserId).IsRequired();
    }
}