using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Tlis.Cms.ImageAssetManagement.Infrastructure.Persistence;

public class ImageAssetManagementDbContext(DbContextOptions options)
    : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseExceptionProcessor();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ImageAssetManagementDbContext).Assembly);
        modelBuilder.HasDefaultSchema("cms_image_asset_management");
        base.OnModelCreating(modelBuilder);
    }
}