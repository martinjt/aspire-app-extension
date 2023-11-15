using AspireDashboard.Extensions;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseSetting(WebHostDefaults.ApplicationKey, "Sample App");

builder.Services.AddOpenTelemetry()
    .ConfigureResource(r => r.AddService("Sample App"))
    .WithTracing(tracerProviderBuilder => {
        tracerProviderBuilder.AddAspNetCoreInstrumentation();
    });

builder.Services.AddAspireDashboard();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
