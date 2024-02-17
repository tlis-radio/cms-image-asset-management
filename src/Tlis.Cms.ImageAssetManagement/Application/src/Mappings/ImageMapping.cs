using Tlis.Cms.ImageAssetManagement.Application.Contracts.Api.Responses;
using Tlis.Cms.ImageAssetManagement.Domain.Entities.Images;

namespace Tlis.Cms.ImageAssetManagement.Application.Mappings;

internal static class ImageMapping
{
    public static ImageGetByIdResponse? ToImageGetByIdRequest(Image? entity)
    {
        if (entity is null)
        {
            return null;
        }

        return new ImageGetByIdResponse
        {
            Id = entity.Id,
            Height = entity.Height,
            Width = entity.Width,
            Url = entity.Url
        };
    }
}