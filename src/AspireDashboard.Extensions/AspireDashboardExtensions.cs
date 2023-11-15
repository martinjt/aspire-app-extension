using Aspire.Dashboard;
using Aspire.Dashboard.Components.Dialogs;
using Aspire.Dashboard.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace AspireDashboard.Extensions;

public static class AspireDashboardExtensions
{
    public static IServiceCollection AddAspireDashboard(this IServiceCollection services)
    {
        services.AddHostedService((sp) => {
            var applicationName = sp.GetRequiredService<IWebHostEnvironment>().ApplicationName;
            var originalUrls = Environment.GetEnvironmentVariable("ASPNETCORE_URLS");
            Environment.SetEnvironmentVariable("ASPNETCORE_URLS", "http://localhost:18888");
            var application = new DashboardWebApplication(sp.GetRequiredService<ILogger<DashboardWebApplication>>(), serviceCollection =>
            {
                serviceCollection.AddSingleton<IDashboardViewModelService>(new DashboardViewModelService(applicationName));
            });

            Environment.SetEnvironmentVariable("ASPNETCORE_URLS", originalUrls);

            return application;
        });

        var exporterUri = new Uri("http://localhost:18889");

        services.AddLogging(options => 
            options.AddOpenTelemetry(openTelemetry => 
                openTelemetry.AddOtlpExporter(o => o.Endpoint = exporterUri)));

        services.AddOpenTelemetry()
            .WithTracing(tracerProviderBuilder => tracerProviderBuilder.AddOtlpExporter(o => o.Endpoint = exporterUri))
            .WithMetrics(metricProviderBuilder => metricProviderBuilder.AddOtlpExporter(o => o.Endpoint = exporterUri));

        return services;
    }
}


public class AspireSettings
{
    public int Port { get; set; }
}