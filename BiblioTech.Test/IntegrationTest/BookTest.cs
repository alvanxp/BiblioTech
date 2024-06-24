using System.Net;
using BiblioTech;
using BiblioTech.Domain.Dto;
using Newtonsoft.Json;

namespace BiblioTech.Test.IntegrationTest
{
    public class BookTest(SqlServerContainer sqlServerContainer) : BaseTest(sqlServerContainer)
    {
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
            var books = JsonConvert.DeserializeObject<List<BookDto>>(stringResponse);

            // Assert
            Assert.Contains(books, b => b.Title == "The Hobbit");
        }
    }
}