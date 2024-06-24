using Testcontainers.MsSql;

namespace BiblioTech.Test.IntegrationTest;

public class SqlServerContainer: IAsyncLifetime
{
    
    private readonly MsSqlContainer _msSqlContainer
        = new MsSqlBuilder().WithImage("alvanxp/bookstore-db-image:latest")
            .WithPassword("abcDEF123#").Build();
    
   public string ConnectionString => _msSqlContainer.GetConnectionString().Replace("master", "BookStore");

   public Task InitializeAsync()
       => _msSqlContainer.StartAsync();

    public Task DisposeAsync()
        => _msSqlContainer.DisposeAsync().AsTask();
}