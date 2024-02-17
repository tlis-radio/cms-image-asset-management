using System.IO;

namespace Tlis.Cms.ImageAssetManagement.Application.Services.Interfaces;

internal interface IImageProcessingService
{
    Stream Resize(Stream stream, int width, int height);
}