using System;

namespace Tlis.Cms.ImageAssetManagement.Infrastructure.Exceptions;

public class EntityNotFoundException(string? message = null) : Exception(message)
{
}