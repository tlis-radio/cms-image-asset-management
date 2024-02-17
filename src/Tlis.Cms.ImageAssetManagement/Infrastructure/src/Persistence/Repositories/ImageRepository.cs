using Tlis.Cms.ImageAssetManagement.Domain.Entities.Images;
using Tlis.Cms.ImageAssetManagement.Infrastructure.Persistence.Repositories.Interfaces;

namespace Tlis.Cms.ImageAssetManagement.Infrastructure.Persistence.Repositories;

internal sealed class ImageRepository(ImageAssetManagementDbContext context)
    : GenericRepository<Image>(context), IImageRepository
{
}