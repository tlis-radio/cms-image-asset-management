using System.Diagnostics;

namespace Tlis.Cms.ShowManagement.Shared;

public static class Telemetry
{
    public static readonly string ServiceName = "cms-image-asset-management-service";

    public static readonly ActivitySource ActivitySource = new(ServiceName);
}