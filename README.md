# Experimental Aspire Dashboard extension

This is an experimental add-on for enabling the Aspire Dashboard in a single application.

## Usage

```shell
dotnet add package AspireDashboard.Extensions --prerelease
```

```csharp
var builder = WebApplication.CreateBuilder(args);

...


builder.Services.AddOpenTelemetry()
    .ConfigureResource(r => r.AddService("Sample App"))
    .WithTracing(tracerProviderBuilder => {
        tracerProviderBuilder.AddAspNetCoreInstrumentation();
    });


builder.Services.AddAspireDashboard();

...

var app = builder.Build();
```

# Known Limitations

* This only works in Web projects due to the missing "StaticWebAssets" target needed in MSbuild (should be a simple fix, but I don't have time.)
* The ports are hardcoded (18888 for the dashboard, 18889 for the OTLP ingest)
* It currently has a feedback loop as th it monitors itself, and generates more traffic because of that, don't use it for a long time!
* You should absolutely wrap this so it only comes into play in Development mode
* Aspire only works on .NET 8