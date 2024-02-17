using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using Tlis.Cms.ImageAssetManagement.Application.Configurations;
using Tlis.Cms.ImageAssetManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.ImageAssetManagement.Application.Services.Interfaces;
using Tlis.Cms.ImageAssetManagement.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.ImageAssetManagement.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.ImageAssetManagement.Application.RequestHandlers;

internal sealed class UserProfileImageUpdateRequestHandler(
    IOptions<ImageProcessingConfiguration> configuration,
    IUnitOfWork unitOfWork,
    IStorageService storageService,
    IImageProcessingService imageProcessingService)
    : IRequestHandler<UserProfileImageUpdateRequest, bool>
{
    private readonly ImageFormatConfiguration _configuration = configuration.Value.User;

    public async Task<bool> Handle(UserProfileImageUpdateRequest request, CancellationToken cancellationToken)
    {
        using var originalImage = request.Image.OpenReadStream();

        using var resizedImage = imageProcessingService.Resize(
            originalImage,
            _configuration.Width,
            _configuration.Height);

        var oldImage = await unitOfWork.ImageRepository.GetByIdAsync(request.Id, asTracking: false);

        if (oldImage is null)
        {
            return false;
        }

         await storageService.UpdateUserProfileImage(resizedImage, request.Id);


        return true;
    }
}