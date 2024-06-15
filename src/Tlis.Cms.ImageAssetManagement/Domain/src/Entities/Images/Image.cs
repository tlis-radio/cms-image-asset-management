using System.Collections.Generic;
using Tlis.Cms.ImageAssetManagement.Domain.Entities.Base;

namespace Tlis.Cms.ImageAssetManagement.Domain.Entities.Images;

public class Image : BaseEntity
{
    public int Width { get; set; }

    public int Height { get; set; }

    public long Size { get; set; }

    public string Url { get; set; } = null!;

    public virtual ICollection<Crop> Crops { get; set; } = [];
}