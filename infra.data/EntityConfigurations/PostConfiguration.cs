using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class PostConfiguration: IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasMany(p => p.PostImages)
            .WithOne(p => p.Post)
            .HasForeignKey(p => p.Id)
            .IsRequired();
    }
}