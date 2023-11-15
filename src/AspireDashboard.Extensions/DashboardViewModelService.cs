using System.Reflection;
using Aspire.Dashboard.Model;

namespace AspireDashboard.Extensions;

public class DashboardViewModelService : IDashboardViewModelService
{
    public string ApplicationName { get; }

    public DashboardViewModelService(string applicationName)
    {
        ApplicationName = applicationName;
    }

    public ValueTask<List<ContainerViewModel>> GetContainersAsync()
    {
        return ValueTask.FromResult(new List<ContainerViewModel>());
    }

    public ValueTask<List<ExecutableViewModel>> GetExecutablesAsync()
    {
        return ValueTask.FromResult(new List<ExecutableViewModel>());
    }

    public ValueTask<List<ProjectViewModel>> GetProjectsAsync()
    {
        return ValueTask.FromResult(new List<ProjectViewModel>());
    }

    public IAsyncEnumerable<ResourceChanged<ContainerViewModel>> WatchContainersAsync(IEnumerable<NamespacedName>? existingContainers = null, CancellationToken cancellationToken = default)
    {
        return AsyncEnumerable.Empty<ResourceChanged<ContainerViewModel>>();
    }

    public IAsyncEnumerable<ResourceChanged<ExecutableViewModel>> WatchExecutablesAsync(IEnumerable<NamespacedName>? existingExecutables = null, CancellationToken cancellationToken = default)
    {
        return AsyncEnumerable.Empty<ResourceChanged<ExecutableViewModel>>();
    }

    public IAsyncEnumerable<ResourceChanged<ProjectViewModel>> WatchProjectsAsync(IEnumerable<NamespacedName>? existingProjects = null, CancellationToken cancellationToken = default)
    {
        return AsyncEnumerable.Empty<ResourceChanged<ProjectViewModel>>();
    }
}
