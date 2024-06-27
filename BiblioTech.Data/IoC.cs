using Microsoft.Extensions.DependencyInjection;

namespace BiblioTech.Data;

public static class IoC
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IBookRepository, BookRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        return services;
    }
} 