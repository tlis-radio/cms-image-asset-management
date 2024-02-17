using System;
using MediatR;
using Microsoft.AspNetCore.Http;
using Tlis.Cms.ImageAssetManagement.Application.Contracts.Api.Responses;

namespace Tlis.Cms.ImageAssetManagement.Application.Contracts.Api.Requests;

public sealed class UserProfileImageCreateRequest : IRequest<BaseCreateResponse>
{
    public Guid UserId { get; set; }

    public required IFormFile Image { get; set; }
}