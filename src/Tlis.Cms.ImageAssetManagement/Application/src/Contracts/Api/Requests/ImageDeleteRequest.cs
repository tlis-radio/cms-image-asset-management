using System;
using MediatR;

namespace Tlis.Cms.ImageAssetManagement.Application.Contracts.Api.Requests;

public sealed class ImageDeleteRequest : IRequest<bool>
{
    public Guid Id { get; set; }
}