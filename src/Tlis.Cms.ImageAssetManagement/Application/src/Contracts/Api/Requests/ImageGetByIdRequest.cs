using System;
using MediatR;
using Tlis.Cms.ImageAssetManagement.Application.Contracts.Api.Responses;

namespace Tlis.Cms.ImageAssetManagement.Application.Contracts.Api.Requests;

public sealed class ImageGetByIdRequest : IRequest<ImageGetByIdResponse?>
{
    public Guid Id { get; set; }


}