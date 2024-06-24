using BiblioTech.Data;
using BiblioTech.Services;
using Moq;

namespace BiblioTech.Test.UnitTest;

public class BookTest
{
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly BookService _bookService;

    public BookTest()
    {
        _bookRepositoryMock = new Mock<IBookRepository>();
        _bookService = new BookService(_bookRepositoryMock.Object);
    }

    [Fact]
    public async Task GetBooks_ReturnsBooks()
    {
        // Arrange
        var books = new List<Book>
        {
            new Book
            {
                Title = "The Hobbit",
                Author = "J.R.R. Tolkien",
                Year = 1937,
                Genre = "Fantasy novel",
                Description = "The Hobbit is a tale of high adventure, undertaken by a company of dwarves in search of dragon-guarded gold."
            }
        };
        _bookRepositoryMock.Setup(x => x.GetBooks()).ReturnsAsync(books);
        
        // Act
        var result = await _bookService.GetBooks();

        // Assert
        Assert.Equal(books.Count, result.Count);
        Assert.Equal(books[0].Title, result[0].Title);
        Assert.Equal(books[0].Author, result[0].Author);
        Assert.Equal(books[0].Year, result[0].Year);
        Assert.Equal(books[0].Genre, result[0].Genre);
        Assert.Equal(books[0].Description, result[0].Description);
    }
}