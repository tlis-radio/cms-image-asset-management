using System.IO;
using NetVips;
using Tlis.Cms.ImageAssetManagement.Application.Services.Interfaces;

namespace Tlis.Cms.ImageAssetManagement.Application.Services;

internal sealed class ImageProcessingService : IImageProcessingService
{
    public Stream Resize(Stream stream, int width, int height)
    {
        using var image = Image.NewFromStream(stream);

        using var resize = image.ThumbnailImage(width, height, crop: Enums.Interesting.Centre);

        var result = new MemoryStream();

        resize.WebpsaveStream(result, lossless: false);

        return result;
    }
}