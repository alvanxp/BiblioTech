using BiblioTech;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Testcontainers.MsSql;

namespace BiblioTech.Test.IntegrationTest;

[Collection("IntegrationTest")]
public abstract class BaseTest 
{
    protected readonly HttpClient Client;

    protected BaseTest(SqlServerContainer sqlServerContainer)
    {
        WebApplicationFactory<Program> webApplicationFactory =
            new WebApplicationFactory<Program>().WithWebHostBuilder(host =>
            {
                host.UseSetting("ConnectionStrings:DefaultConnection", sqlServerContainer.ConnectionString);
            });
        Client = webApplicationFactory.CreateClient();
    }

    public void Dispose()
    {
        Client.Dispose();
    }

}