using Bz.Fott.Administration.Application.Common;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Bz.Fott.Administration.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        return service
            .AddMediatR(
                typeof(Application.Common.IUnitOfWork),
                typeof(Domain.Common.IDomainEvent))
            .AddApplicationServices();
    }

    private static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        return services
            .Scan(scan => scan.FromAssemblyOf<IApplicationService>()
            .AddClasses(classes => classes.AssignableTo<IApplicationService>())
            .AsMatchingInterface()
            .WithScopedLifetime());
    }
}
