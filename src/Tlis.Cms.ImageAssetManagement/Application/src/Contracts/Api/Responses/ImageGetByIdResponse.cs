using System;

namespace Tlis.Cms.ImageAssetManagement.Application.Contracts.Api.Responses;

public sealed class ImageGetByIdResponse
{
    public Guid Id { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }

    public required string Url { get; set; }
}