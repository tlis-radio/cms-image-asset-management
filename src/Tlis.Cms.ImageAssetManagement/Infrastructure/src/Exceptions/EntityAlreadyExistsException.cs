using System;

namespace Tlis.Cms.ImageAssetManagement.Infrastructure.Exceptions;

public class EntityAlreadyExistsException(string? message = null) : Exception(message)
{
}