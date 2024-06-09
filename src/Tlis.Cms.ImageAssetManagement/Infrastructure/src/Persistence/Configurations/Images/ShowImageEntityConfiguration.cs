using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tlis.Cms.ImageAssetManagement.Domain.Entities.Images;

namespace Tlis.Cms.ImageAssetManagement.Infrastructure.Persistence.Configurations.Images;

public class ShowImageEntityConfiguration : IEntityTypeConfiguration<ShowImage>
{
    public void Configure(EntityTypeBuilder<ShowImage> builder)
    {
        builder.Property(x => x.ShowId).IsRequired();

        builder.HasIndex(x => x.ShowId);
    }
}