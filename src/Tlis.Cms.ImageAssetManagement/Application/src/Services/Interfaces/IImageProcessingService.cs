using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Tlis.Cms.ImageAssetManagement.Domain.Entities.Images;

namespace Tlis.Cms.ImageAssetManagement.Application.Services.Interfaces;

internal interface IImageProcessingService
{
    Task<Image> CreateUserImageAsync(IFormFile image, Guid userId);

    Task<Image> CreateShowImageAsync(IFormFile image, Guid showId);
}