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

internal sealed class BroadcastImageCreateRequestHandler(
    IUnitOfWork unitOfWork,
    IImageProcessingService imageProcessingService,
    IOptions<ImageProcessingConfiguration> imageProcessingConfiguration)
    : IRequestHandler<BroadcastImageCreateRequest, BaseCreateResponse>
{
    public async Task<BaseCreateResponse> Handle(BroadcastImageCreateRequest request, CancellationToken cancellationToken)
    {
        var broadcastImage = await imageProcessingService.CreateImageAsync(request.Image, imageProcessingConfiguration.Value.Broadcast);

        await unitOfWork.ImageRepository.InsertAsync(broadcastImage);
        await unitOfWork.SaveChangesAsync();

        return new BaseCreateResponse { Id = broadcastImage.Id };
    }
}