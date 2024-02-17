using System;
using System.Linq;
using System.Threading.Tasks;
using EntityFramework.Exceptions.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tlis.Cms.ImageAssetManagement.Infrastructure.Exceptions;
using Tlis.Cms.ImageAssetManagement.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.ImageAssetManagement.Infrastructure.Persistence.Repositories;
using Tlis.Cms.ImageAssetManagement.Infrastructure.Persistence.Repositories.Interfaces;

namespace Tlis.Cms.ImageAssetManagement.Infrastructure.Persistence;

public class UnitOfWork : IDisposable, IUnitOfWork
{
    public IImageRepository ImageRepository => _lazyImageRepository.Value;

    private bool _disposed;

    private readonly ILogger<UnitOfWork> _logger;

    private readonly ImageAssetManagementDbContext _dbContext;

    private readonly Lazy<IImageRepository> _lazyImageRepository;

    public UnitOfWork(ImageAssetManagementDbContext dbContext, ILogger<UnitOfWork> logger)
    {
        _dbContext = dbContext;
        _lazyImageRepository = new(() => new ImageRepository(_dbContext));
        _logger = logger;
    }

    public void SetStateUnchanged<TEntity>(params TEntity[] entities) where TEntity : class
    {
        foreach (var entity in entities)
        {
            _dbContext.Entry(entity).State = EntityState.Unchanged;
        }
    }

    public async Task SaveChangesAsync()
    {
        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception exception)
        {
            _logger.LogError("{Exception}", exception.Message);
            
            throw exception switch
            {
                UniqueConstraintException => new EntityAlreadyExistsException(),
                DbUpdateConcurrencyException => new EntityNotFoundException(),
                _ => new Exception(exception.Message)
            };
        }
    }

    public async Task ExecutePendingMigrationsAsync()
    {
        var pendingMigrations = (await _dbContext.Database.GetPendingMigrationsAsync()).ToList();

        if (pendingMigrations.Any())
        {
            _logger.LogInformation("Applying migrations: {Join}", string.Join(',', pendingMigrations));

            await _dbContext.Database.MigrateAsync();
        }
        else
        {
            _logger.LogInformation("No migrations to execute");
        }
    }

    public void Dispose()
    {
        if (!_disposed)
            _dbContext.Dispose();

        _disposed = true;
        GC.SuppressFinalize(this);
    }
}