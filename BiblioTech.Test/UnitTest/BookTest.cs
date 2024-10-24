using BiblioTech.Data;
using BiblioTech.Domain.Dto;
using BiblioTech.Domain.Entities;
using BiblioTech.Services;
using BiblioTech.Services.BookService;
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
                Genre = "Fantasy novel",
                Description = "The Hobbit is a tale of high adventure, undertaken by a company of dwarves in search of dragon-guarded gold.",
                PublishDate = DateTime.Now,
                Price = (decimal)10.99,
                ISBN = "978-0-395-08302-2"
            }
        };
        _bookRepositoryMock.Setup(x => x.GetBooks()).ReturnsAsync(books);

        // Act
        var response = await _bookService.GetBooks();
        List<BookResponse> result = response.Data;

        // Assert
        Assert.Equal(books.Count, result.Count);
        Assert.Equal(books[0].Title, result[0].Title);
        Assert.Equal(books[0].Author, result[0].Author);
        Assert.Equal(books[0].Genre, result[0].Genre);
        Assert.Equal(books[0].Description, result[0].Description);
        Assert.Equal(books[0].PublishDate, result[0].PublishDate);
        Assert.Equal(books[0].Price, result[0].Price);
        Assert.Equal(books[0].ISBN, result[0].ISBN);
    }
    //Add add book  method
    [Fact]
    public async Task AddBook_ReturnsBook()
    {
        // Arrange
        bool success = false;
        var book = new BookRequest(null, "The Hobbit", "J.R.R. Tolkien", "Fantasy novel",
            "The Hobbit is a tale of high adventure, undertaken by a company of dwarves in search of dragon-guarded gold.", DateTime.Now, (decimal)10.99, "978-0-395-08302-2");
        _bookRepositoryMock.Setup(x => x.AddBook(It.IsAny<Book>())).ReturnsAsync(success);

        // Act
        var result = await _bookService.AddBook(book);

        // Assert
        Assert.NotNull(result);
    }
    //test add book method with null book
    [Fact]
    public async Task AddBook_ReturnsFalse()
    {
        // Arrange
        bool success = false;
        BookRequest? book = null;
        _bookRepositoryMock.Setup(x => x.AddBook(It.IsAny<Book>())).ReturnsAsync(success);

        // Act

        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(async () =>
        {
            await _bookService.AddBook(book);
        });
    }

    //test add get book by id method
    [Fact]
    public async Task GetBookById_ReturnsBook()
    {
        // Arrange
        var book = new Book
        {
            Title = "The Hobbit",
            Author = "J.R.R. Tolkien",
            Genre = "Fantasy novel",
            Description = "The Hobbit is a tale of high adventure, undertaken by a company of dwarves in search of dragon-guarded gold."
        };
        _bookRepositoryMock.Setup(x => x.GetBookById(It.IsAny<int>())).ReturnsAsync(book);

        // Act
        var response = await _bookService.GetBookById(1);
        var result = response.Data;

        // Assert
        Assert.Equal(book.Title, result.Title);
        Assert.Equal(book.Author, result.Author);
        Assert.Equal(book.Genre, result.Genre);
        Assert.Equal(book.Description, result.Description);
        Assert.Equal(book.PublishDate, result.PublishDate);
        Assert.Equal(book.Price, result.Price);
        Assert.Equal(book.ISBN, result.ISBN);
    }
}