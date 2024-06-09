using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Tlis.Cms.ImageAssetManagement.Domain.Entities.Images;

namespace Tlis.Cms.ImageAssetManagement.Application.Services.Interfaces;

internal interface IImageProcessingService
{
    Task<UserProfileImage> CreateUserImageAsync(IFormFile image, Guid userId);

    Task<ShowImage> CreateShowImageAsync(IFormFile image, Guid showId);
}