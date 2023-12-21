using Aspire.Dashboard.Model;
using Microsoft.AspNetCore.Hosting;

namespace AspireDashboard.Extensions;

public class DashboardViewModelService(IWebHostEnvironment webHostEnvironment) : IDashboardViewModelService
{
    public string ApplicationName { get; } = webHostEnvironment.ApplicationName;

    public ViewModelMonitor<ResourceViewModel> GetResources() => new ViewModelProcessor<ResourceViewModel>().GetResourceMonitor();
}

internal sealed class ViewModelProcessor<T>
    where T : ResourceViewModel
{
    private readonly Dictionary<string, T> snapshot = [];

    public ViewModelMonitor<T> GetResourceMonitor() => new([.. snapshot.Values], AsyncEnumerable.Empty<ResourceChanged<T>>());
}
