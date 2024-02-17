using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tlis.Cms.ImageAssetManagement.Domain.Entities.Images;

namespace Tlis.Cms.ImageAssetManagement.Infrastructure.Persistence.Configurations.Images;

public class UserProfileImageEntityConfiguration : IEntityTypeConfiguration<UserProfileImage>
{
    public void Configure(EntityTypeBuilder<UserProfileImage> builder)
    {
        builder.Property(x => x.UserId).IsRequired();

        builder.HasIndex(x => x.UserId);
    }
}