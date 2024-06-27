using BiblioTech.Services.BookService;
using BiblioTech.Services.UserService;
using Microsoft.Extensions.DependencyInjection;

namespace BiblioTech.Services;

public static class IoC
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService.UserService>();
        services.AddScoped<IBookService, BookService.BookService>();
    }
}