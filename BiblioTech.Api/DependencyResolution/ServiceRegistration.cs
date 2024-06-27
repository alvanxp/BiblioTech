using BiblioTech.Data;
using BiblioTech.Services.BookService;
using BiblioTech.Services.UserService;

namespace BiblioTech.DependencyResolution;

public static class ServiceRegistration
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IBookService, BookService>();
    }
}