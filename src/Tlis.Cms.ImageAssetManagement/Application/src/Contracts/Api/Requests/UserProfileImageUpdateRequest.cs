using System;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Tlis.Cms.ImageAssetManagement.Application.Contracts.Api.Requests;

public sealed class UserProfileImageUpdateRequest : IRequest<bool>
{
    public Guid Id { get; set; }

    public required IFormFile Image { get; set; }
}