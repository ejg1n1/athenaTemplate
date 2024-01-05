using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class ApplicationUserRoleConfig: IEntityTypeConfiguration<ApplicationUserRole>
{
    public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
    {
        // Primary key
        builder.HasKey(r => new { r.UserId, r.RoleId });

        builder.HasOne(o => o.User).WithMany(o => o.UserRoles).HasForeignKey(o => o.UserId);
        builder.HasOne(o => o.Role).WithMany(o => o.UserRoles).HasForeignKey(o => o.RoleId);
    }
}