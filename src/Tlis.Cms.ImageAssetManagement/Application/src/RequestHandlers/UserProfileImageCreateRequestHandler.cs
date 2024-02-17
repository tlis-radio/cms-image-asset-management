using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using Tlis.Cms.ImageAssetManagement.Application.Configurations;
using Tlis.Cms.ImageAssetManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.ImageAssetManagement.Application.Contracts.Api.Responses;
using Tlis.Cms.ImageAssetManagement.Application.Services.Interfaces;
using Tlis.Cms.ImageAssetManagement.Domain.Entities;
using Tlis.Cms.ImageAssetManagement.Domain.Entities.Images;
using Tlis.Cms.ImageAssetManagement.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.ImageAssetManagement.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.ImageAssetManagement.Application.RequestHandlers;

internal sealed class UserProfileImageCreateRequestHandler(
    IOptions<ImageProcessingConfiguration> configuration,
    IUnitOfWork unitOfWork,
    IStorageService storageService,
    IImageProcessingService imageProcessingService)
    : IRequestHandler<UserProfileImageCreateRequest, BaseCreateResponse>
{
    private readonly ImageFormatConfiguration _configuration = configuration.Value.User;

    public async Task<BaseCreateResponse> Handle(UserProfileImageCreateRequest request, CancellationToken cancellationToken)
    {
        using var originalImage = request.Image.OpenReadStream();

        using var resizedImage = imageProcessingService.Resize(
            originalImage,
            _configuration.Width,
            _configuration.Height);

        var (imageId, url) = await storageService.UploadUserProfileImage(resizedImage);

        var image = new UserProfileImage
        {
            Id = imageId,
            UserId = request.UserId,
            Width = _configuration.Width,
            Height = _configuration.Height,
            Url = url
        };

        await unitOfWork.ImageRepository.InsertAsync(image);
        await unitOfWork.SaveChangesAsync();

        return new BaseCreateResponse { Id = imageId };
    }
}