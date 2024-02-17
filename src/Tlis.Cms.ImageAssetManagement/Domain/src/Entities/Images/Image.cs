using System;
using Tlis.Cms.ImageAssetManagement.Domain.Entities.Base;

namespace Tlis.Cms.ImageAssetManagement.Domain.Entities.Images;

public abstract class Image : BaseEntity
{
    public int Width { get; set; }

    public int Height { get; set; }

    public string Url { get; set; } = null!;
}