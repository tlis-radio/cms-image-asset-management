using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.ImageAssetManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.ImageAssetManagement.Application.Exceptions;
using Tlis.Cms.ImageAssetManagement.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.ImageAssetManagement.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.ImageAssetManagement.Application.RequestHandlers;

internal sealed class ImageDeleteRequestHandler(
    IUnitOfWork unitOfWork,
    IStorageService storageService)
    : IRequestHandler<ImageDeleteRequest, bool>
{
    public async Task<bool> Handle(ImageDeleteRequest request, CancellationToken cancellationToken)
    {
        var image = await unitOfWork.ImageRepository.GetByIdAsync(request.Id, asTracking: false);

        if (image is null)
        {
            return false;
        }

        if (await storageService.DeleteImage(image.Url) is false)
        {
            throw new UnableToDeleteFromStorageException();
        }

        await unitOfWork.ImageRepository.DeleteByIdAsync(request.Id);
        await unitOfWork.SaveChangesAsync();

        return true;
    }
}