using System;
using System.IO;
using System.Threading.Tasks;

namespace Tlis.Cms.ImageAssetManagement.Infrastructure.Services.Interfaces;

public interface IStorageService
{
    public Task<bool> DeleteUserProfileImage(string fileUrl);

    public Task<(Guid, string)> UploadUserProfileImage(Stream stream);

    Task UpdateUserProfileImage(Stream stream, Guid imageId);
}