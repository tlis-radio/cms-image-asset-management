using System;

namespace Tlis.Cms.ImageAssetManagement.Domain.Entities.Images;

public class UserProfileImage : Image
{
    public Guid UserId { get; set; }
}