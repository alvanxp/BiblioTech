using System.Net;
using System.Net.Http.Json;
using System.Text;
using BiblioTech.Domain.Dto;
using Newtonsoft.Json;

namespace BiblioTech.Test.IntegrationTest
{
    public class BookTest : BaseTest
    {
        public BookTest(SqlServerContainer sqlServerContainer) : base(sqlServerContainer)
        {
            var token = GetToken().GetAwaiter().GetResult();
            Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        private  async Task<string> GetToken()
        {
            var url = "api/login/register";
            var model = new RegisterRequest()
            {
                Username = Guid.NewGuid().ToString(),
                Password = "testpassword",
                FirstName = "John",
                LastName = "Doe"
            };

            var response = await Client.PostAsJsonAsync(url, model);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<AuthenticateResponse>(stringResponse);

            return user.Token;
        }

        [Fact]
        public async Task GetBooks_ReturnsSuccessStatusCode()
        {
            // Arrange
            var url = "api/Books";
        
            // Act
            var response = await Client.GetAsync(url);
        
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetBooks_ReturnsExpectedBooks()
        {
            // Arrange
            var url = "api/Books";

            // Act
            var response = await Client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var books = JsonConvert.DeserializeObject<List<BookResponse>>(stringResponse);

            // Assert
            Assert.Contains(books, b => b.Title == "The Hobbit");
        }

        [Fact]
        public async Task GetBooks_ReturnsUnAuthorized()
        {
            // Arrange
            var url = "api/Books";

            // Act
            Client.DefaultRequestHeaders.Authorization = null;
            var response = await Client.GetAsync(url);
            
            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
        
        [Fact]
        public async Task AddBook_ReturnsSuccessStatusCode()
        {
            // Arrange
            var url = "api/Books";
            var book = new BookResponse(null, "C# in Depth", "Jon Skeet", "Technical",
                "C# in Depth is a book that dives into the C# language and its nuances. It provides a deep understanding of C# programming, covering C# 6.0 and 7.0 versions with a preview of C# 8.0.",
                DateTime.Now, (decimal)39.99, "978-1617294532");
            var content = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json");

            // Act
            var response = await Client.PostAsync(url, content);
            var queryResponse = await Client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var stringResponse = await queryResponse.Content.ReadAsStringAsync();
            var books = JsonConvert.DeserializeObject<List<BookResponse>>(stringResponse);
            
            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Contains(books, b => b.Title == "C# in Depth");
        }
    }
}