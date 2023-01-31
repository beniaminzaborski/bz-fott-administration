namespace Bz.Fott.Administration.WebAPI;

public static class DependencyInjection
{
    public static IServiceCollection AddCustomControllers(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            //options.Filters.Add(typeof(GlobalExceptionFilter));
            options.SuppressAsyncSuffixInActionNames = false;
        });

        return services;
    }
}
