using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.ImageAssetManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.ImageAssetManagement.Application.Contracts.Api.Responses;
using Tlis.Cms.ImageAssetManagement.Application.Mappings;
using Tlis.Cms.ImageAssetManagement.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.ImageAssetManagement.Application.RequestHandlers;

internal sealed class ImageGetByIdRequestHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<ImageGetByIdRequest, ImageGetByIdResponse?>
{
    public async Task<ImageGetByIdResponse?> Handle(ImageGetByIdRequest request, CancellationToken cancellationToken)
    {
        var response = await unitOfWork.ImageRepository.GetByIdAsync(request.Id, asTracking: false);

        return ImageMapping.ToImageGetByIdRequest(response);
    }
}