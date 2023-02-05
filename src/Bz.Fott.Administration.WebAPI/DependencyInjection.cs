using Bz.Fott.Administration.WebAPI.ExceptionsHandling;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;
using OpenTelemetry;
using Npgsql;
using Microsoft.Extensions.Configuration;

namespace Bz.Fott.Administration.WebAPI;

public static class DependencyInjection
{
    public static IServiceCollection AddCustomControllers(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add(typeof(GlobalExceptionFilter));
            options.SuppressAsyncSuffixInActionNames = false;
        });

        return services;
    }

    public static IServiceCollection AddTelemetry(this IServiceCollection services, string serviceName, string serviceVersion)
    {
        return services
            .AddOpenTelemetry()
            .WithTracing(builder => builder
                .AddSource(serviceName)
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName, serviceVersion: serviceVersion))
                .AddAspNetCoreInstrumentation()
                .AddNpgsql()
                .AddConsoleExporter()
                .AddOtlpExporter())
            .WithMetrics(builder => builder
                .AddMeter(serviceName)
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName, serviceVersion: serviceVersion))
                .AddRuntimeInstrumentation()
                .AddAspNetCoreInstrumentation()
                .AddConsoleExporter()
                .AddOtlpExporter())
            .StartWithHost()
            .Services;
    }
}
