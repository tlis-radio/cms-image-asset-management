using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using Tlis.Cms.ImageAssetManagement.Application.Configurations;
using Tlis.Cms.ImageAssetManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.ImageAssetManagement.Application.Contracts.Api.Responses;
using Tlis.Cms.ImageAssetManagement.Application.Services.Interfaces;
using Tlis.Cms.ImageAssetManagement.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.ImageAssetManagement.Application.RequestHandlers;

internal sealed class UserProfileImageCreateRequestHandler(
    IUnitOfWork unitOfWork,
    IImageProcessingService imageProcessingService,
    IOptions<ImageProcessingConfiguration> imageProcessingConfiguration)
    : IRequestHandler<UserProfileImageCreateRequest, BaseCreateResponse>
{
    public async Task<BaseCreateResponse> Handle(UserProfileImageCreateRequest request, CancellationToken cancellationToken)
    {
        var userImage = await imageProcessingService.CreateImageAsync(request.Image, imageProcessingConfiguration.Value.User);

        await unitOfWork.ImageRepository.InsertAsync(userImage);
        await unitOfWork.SaveChangesAsync();

        return new BaseCreateResponse { Id = userImage.Id };
    }
}