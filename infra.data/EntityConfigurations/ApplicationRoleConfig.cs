using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class ApplicationRoleConfig: IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        // Primary key
        builder.HasKey(r => r.Id);

        // Index for "normalized" role name to allow efficient lookups
        builder.HasIndex(r => r.NormalizedName).HasDatabaseName("RoleNameIndex")
            .IsUnique();

        // A concurrency token for use with the optimistic concurrency checking
        builder.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

        // Limit the size of columns to use efficient database types
        builder.Property(u => u.Name).HasMaxLength(256);
        builder.Property(u => u.NormalizedName).HasMaxLength(256);

        // The relationships between Role and other entity types
        // Note that these relationships are configured with no navigation properties

        // Each Role can have many entries in the UserRole join table
        builder.HasMany(o => o.UserRoles)
            .WithOne(o => o.Role)
            .HasForeignKey(ur => ur.RoleId).IsRequired();

        // Each Role can have many associated RoleClaims
        builder.HasMany(o => o.RoleClaims)
            .WithOne(o => o.Role)
            .HasForeignKey(rc => rc.RoleId).IsRequired();
    }
}