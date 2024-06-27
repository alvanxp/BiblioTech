using BiblioTech.Data;
using BiblioTech.DependencyResolution;
using BiblioTech.Services;
using BiblioTech.Services.BookService;
using BiblioTech.Services.UserService;
using Microsoft.OpenApi.Models;

namespace BiblioTech;
public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerServices();
        builder.Services.AddControllers();
        builder.Services.Configure<ConnectionString>(builder.Configuration.GetSection("ConnectionStrings"));
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
        builder.Services.AddServices();
        builder.AddJwtAuthentication();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
       // if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.MapControllers();
        app.UseAuthorization();
        app.Run();
    }


}