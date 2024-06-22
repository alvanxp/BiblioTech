var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/Books", () =>
{
  return new List<BookDto>{new BookDto("The Hobbit", "J.R.R. Tolkien", 1937, "Fantasy novel", "The Hobbit is a tale of high adventure, undertaken by a company of dwarves in search of dragon-guarded gold.")};
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record BookDto(string name, string author, int year, string genre, string description);
