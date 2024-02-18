using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Tlis.Cms.ImageAssetManagement.Domain.Entities.Images;

namespace Tlis.Cms.ImageAssetManagement.Infrastructure.Persistence.Configurations.Images;

public class CropEntityConfiguration : IEntityTypeConfiguration<Crop>
{
    public void Configure(EntityTypeBuilder<Crop> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd().HasValueGenerator((_, _) => new GuidValueGenerator());

        builder.Property(x => x.Width).IsRequired();
        builder.Property(x => x.Height).IsRequired();
        builder.Property(x => x.Url).IsRequired();
        builder.Property(x => x.Size).IsRequired();

        builder.HasIndex(x => x.ImageId);
    }
}