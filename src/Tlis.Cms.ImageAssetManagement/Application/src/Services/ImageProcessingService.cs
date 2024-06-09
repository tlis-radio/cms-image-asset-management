using System;
using System.Drawing;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Tlis.Cms.ImageAssetManagement.Application.Configurations;
using Tlis.Cms.ImageAssetManagement.Application.Services.Interfaces;
using Tlis.Cms.ImageAssetManagement.Domain.Entities.Images;
using Tlis.Cms.ImageAssetManagement.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.ImageAssetManagement.Application.Services;

internal sealed class ImageProcessingService(
    IStorageService storageService,
    IImageService imageService,
    IOptions<ImageProcessingConfiguration> configuration)
    : IImageProcessingService
{
    private readonly ImageProcessingConfiguration _configuration = configuration.Value;

public async Task<ShowImage> CreateShowImageAsync(IFormFile image, Guid showId)
    {
        using var originalImageStream = image.OpenReadStream();
        var originalImageId = Guid.NewGuid();
        var cropImageId = Guid.NewGuid();

        var (
            originalImage,
            originalFileSize,
            originalImageSize,
            originalImageWebpUrl
        ) = await ProcessOriginalImageAsync(image, originalImageId);

        using (originalImage)
        {
            var (
                croppedFileSize,
                croppedWebpUrl
            ) = await CropImageAsync(originalImage, originalImageSize, _configuration.Show, cropImageId);

            return new ShowImage
            {
                Id = originalImageId,
                ShowId = showId,
                Width = originalImageSize.Width,
                Height = originalImageSize.Height,
                Url = originalImageWebpUrl,
                Size = originalFileSize,
                Crops = [
                    new Crop
                    {
                        Id = cropImageId,
                        Height = _configuration.Show.Height,
                        Width = _configuration.Show.Width,
                        Size = croppedFileSize,
                        Url = croppedWebpUrl
                    }
                ]
            };
        }
    }

    public async Task<UserProfileImage> CreateUserImageAsync(IFormFile image, Guid userId)
    {
        using var originalImageStream = image.OpenReadStream();
        var originalImageId = Guid.NewGuid();
        var cropImageId = Guid.NewGuid();

        var (
            originalImage,
            originalFileSize,
            originalImageSize,
            originalImageWebpUrl
        ) = await ProcessOriginalImageAsync(image, originalImageId);

        using (originalImage)
        {
            var (
                croppedFileSize,
                croppedWebpUrl
            ) = await CropImageAsync(originalImage, originalImageSize, _configuration.User, cropImageId);

            return new UserProfileImage
            {
                Id = originalImageId,
                UserId = userId,
                Width = originalImageSize.Width,
                Height = originalImageSize.Height,
                Url = originalImageWebpUrl,
                Size = originalFileSize,
                Crops = [
                    new Crop
                    {
                        Id = cropImageId,
                        Height = _configuration.User.Height,
                        Width = _configuration.User.Width,
                        Size = croppedFileSize,
                        Url = croppedWebpUrl
                    }
                ]
            };
        }
    }

    public async Task<(NetVips.Image image, long fileSize, Size imageSize, string url)> ProcessOriginalImageAsync(IFormFile imageFile, Guid imageId)
    {
        using var imageStream = imageFile.OpenReadStream();

        var image = imageService.ToImage(imageStream);
        var imageSize = imageService.GetSize(image);
        using var webp = imageService.ToWebp(image);
        var webpUrl = await storageService.UploadUserProfileImage(webp, imageId);

        return new (image, webp.Length, imageSize, webpUrl);
    }

    public async Task<(long fileSize, string url)> CropImageAsync(NetVips.Image originalImage, Size originalImageSize, ImageFormatConfiguration configuration, Guid imageId)
    {
        using var croppedImage = imageService.Crop(
            originalImage,
            default,
            default,
            originalImageSize.Width,
            originalImageSize.Height);

        using var resizedCroppedImage = imageService.Resize(
            croppedImage,
            configuration.Width,
            configuration.Height);

        using var webp = imageService.ToWebp(resizedCroppedImage);
        
        var webpUrl = await storageService.UploadUserProfileImage(webp, imageId);

        return (webp.Length, webpUrl);
    }
}