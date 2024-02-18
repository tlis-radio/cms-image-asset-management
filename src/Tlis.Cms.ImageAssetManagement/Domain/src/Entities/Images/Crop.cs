using System;
using Tlis.Cms.ImageAssetManagement.Domain.Entities.Base;

namespace Tlis.Cms.ImageAssetManagement.Domain.Entities.Images;

public class Crop : BaseEntity
{
    public int Width { get; set; }

    public int Height { get; set; }

    public long Size { get; set; }

    public string Url { get; set; } = null!;

    public Guid ImageId { get; set; }
}