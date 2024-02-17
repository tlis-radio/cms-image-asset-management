using System.Threading.Tasks;
using Tlis.Cms.ImageAssetManagement.Infrastructure.Persistence.Repositories.Interfaces;

namespace Tlis.Cms.ImageAssetManagement.Infrastructure.Persistence.Interfaces;

public interface IUnitOfWork
{
    public IImageRepository ImageRepository { get; }

    void SetStateUnchanged<TEntity>(params TEntity[] entities) where TEntity : class;

    /// <exception cref="EntityAlreadyExistsException">Thrown when a unique constraint is violated</exception>
    /// <exception cref="ApiException">Thrown when an error occurs while saving changes</exception>
    public Task SaveChangesAsync();

    public Task ExecutePendingMigrationsAsync();
}