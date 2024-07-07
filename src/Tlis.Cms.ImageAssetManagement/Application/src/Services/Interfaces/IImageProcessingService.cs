using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Tlis.Cms.ImageAssetManagement.Application.Configurations;
using Tlis.Cms.ImageAssetManagement.Domain.Entities.Images;

namespace Tlis.Cms.ImageAssetManagement.Application.Services.Interfaces;

internal interface IImageProcessingService
{
    Task<Image> CreateImageAsync(IFormFile image, ImageFormatConfiguration format);
}