using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Tlis.Cms.ImageAssetManagement.Domain.Entities.Images;

namespace Tlis.Cms.ImageAssetManagement.Infrastructure.Persistence.Configurations.Images;

public class RoleEntityConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.UseTptMappingStrategy();

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd().HasValueGenerator((_, _) => new GuidValueGenerator());

        builder.Property(x => x.Width).IsRequired();
        builder.Property(x => x.Height).IsRequired();
        builder.Property(x => x.Url).IsRequired();
    }
}