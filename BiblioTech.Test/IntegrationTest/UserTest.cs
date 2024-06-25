using System.Net;
using System.Net.Http.Json;
using System.Text;
using BiblioTech.Domain.Dto;
using Newtonsoft.Json;

namespace BiblioTech.Test.IntegrationTest;

public class UserTest(SqlServerContainer sqlServerContainer) : BaseTest(sqlServerContainer)
{
    [Fact]
    public async Task Authenticate_InvalidCredentials_ReturnsBadRequest()
    {
        // Arrange
        var url = "api/login/authenticate";
        var model = new AuthenticateRequest
        {
            Username = "testuser2",
            Password = "testpassword2"
        };

        // Act
        var response = await Client.PostAsJsonAsync(url, model);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    
    [Fact]
    public async Task Register_ReturnsSuccessStatusCode()
    {
        // Arrange
        var url = "api/login/register";
        var model = new RegisterRequest
        {
            FirstName = "John",
            LastName = "Doe",
            Username = "johndoe",
            Password = "password"
        };
        var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

        // Act
        var response = await Client.PostAsync(url, content);
        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        
        //request to /api/login/authenticate with the same username and password
        var authenticateUrl = "api/login/authenticate";
        var authenticateModel = new AuthenticateRequest
        {
            Username = "johndoe",
            Password = "password"
        };
        var authenticateContent = new StringContent(JsonConvert.SerializeObject(authenticateModel), Encoding.UTF8, "application/json");
        var authenticateResponse = await Client.PostAsync(authenticateUrl, authenticateContent);
        authenticateResponse.EnsureSuccessStatusCode();
        var authenticateStringResponse = await authenticateResponse.Content.ReadAsStringAsync();
        var authenticateUser = JsonConvert.DeserializeObject<AuthenticateResponse>(authenticateStringResponse);
        
        // Assert
        Assert.NotNull(authenticateUser);
        Assert.NotNull(authenticateUser.Token);
        
    }
}