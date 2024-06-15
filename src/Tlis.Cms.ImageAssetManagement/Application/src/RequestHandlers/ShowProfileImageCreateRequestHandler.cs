using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.ImageAssetManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.ImageAssetManagement.Application.Contracts.Api.Responses;
using Tlis.Cms.ImageAssetManagement.Application.Services.Interfaces;
using Tlis.Cms.ImageAssetManagement.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.ImageAssetManagement.Application.RequestHandlers;

internal sealed class ShowProfileImageCreateRequestHandler(
    IUnitOfWork unitOfWork,
    IImageProcessingService imageProcessingService)
    : IRequestHandler<ShowProfileImageCreateRequest, BaseCreateResponse>
{
    public async Task<BaseCreateResponse> Handle(ShowProfileImageCreateRequest request, CancellationToken cancellationToken)
    {
        var showImage = await imageProcessingService.CreateShowImageAsync(request.Image, request.ShowId);

        await unitOfWork.ImageRepository.InsertAsync(showImage);
        await unitOfWork.SaveChangesAsync();

        return new BaseCreateResponse { Id = showImage.Id };
    }
}