using konyvtar_kezelo_backend.Data;
using konyvtar_kezelo_backend.Models;
using konyvtar_kezelo_backend.Services;
using Microsoft.EntityFrameworkCore;

namespace konyvtar_kezelo_test;

public class BookServiceTests
{
    private LibraryDBContext CreateDb()
    {
        var options = new DbContextOptionsBuilder<LibraryDBContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new LibraryDBContext(options);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllBooks()
    {
        var db = CreateDb();
        var service = new BookService(db);
        await service.CreateAsync(new Book { Title = "Harry Potter", Author = "Rowling", Publisher = "Bloomsbury", ReleaseYear = 1997 });
        await service.CreateAsync(new Book { Title = "Gyűrűk Ura", Author = "Tolkien", Publisher = "Allen", ReleaseYear = 1954 });

        var result = await service.GetAllAsync();

        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetByIdAsync_ValidId_ReturnsBook()
    {
        var db = CreateDb();
        var service = new BookService(db);
        var created = await service.CreateAsync(new Book { Title = "Harry Potter", Author = "Rowling", Publisher = "Bloomsbury", ReleaseYear = 1997 });

        var result = await service.GetByIdAsync(created.Id);

        Assert.NotNull(result);
        Assert.Equal("Harry Potter", result.Title);
    }

    [Fact]
    public async Task GetByIdAsync_InvalidId_ReturnsNull()
    {
        var db = CreateDb();
        var service = new BookService(db);

        var result = await service.GetByIdAsync(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_ValidBook_ReturnsBook()
    {
        var db = CreateDb();
        var service = new BookService(db);

        var result = await service.CreateAsync(new Book { Title = "Harry Potter", Author = "Rowling", Publisher = "Bloomsbury", ReleaseYear = 1997 });

        Assert.NotNull(result);
        Assert.Equal("Harry Potter", result.Title);
    }

    [Fact]
    public async Task UpdateAsync_ValidId_ReturnsUpdatedBook()
    {
        var db = CreateDb();
        var service = new BookService(db);
        var created = await service.CreateAsync(new Book { Title = "Harry Potter", Author = "Rowling", Publisher = "Bloomsbury", ReleaseYear = 1997 });

        var result = await service.UpdateAsync(created.Id, new Book { Title = "Frissített cím", Author = "Rowling", Publisher = "Bloomsbury", ReleaseYear = 1997 });

        Assert.NotNull(result);
        Assert.Equal("Frissített cím", result.Title);
    }

    [Fact]
    public async Task UpdateAsync_InvalidId_ReturnsNull()
    {
        var db = CreateDb();
        var service = new BookService(db);

        var result = await service.UpdateAsync(999, new Book { Title = "Valami", Author = "Valaki", Publisher = "Kiadó", ReleaseYear = 2000 });

        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteAsync_ValidId_ReturnsTrue()
    {
        var db = CreateDb();
        var service = new BookService(db);
        var created = await service.CreateAsync(new Book { Title = "Harry Potter", Author = "Rowling", Publisher = "Bloomsbury", ReleaseYear = 1997 });

        var result = await service.DeleteAsync(created.Id);

        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAsync_InvalidId_ReturnsFalse()
    {
        var db = CreateDb();
        var service = new BookService(db);

        var result = await service.DeleteAsync(999);

        Assert.False(result);
    }
    [Fact]
    public async Task GetAvailableAsync_ReturnsOnlyAvailableBooks()
    {
        var db = CreateDb();
        var service = new BookService(db);

        var book1 = await service.CreateAsync(new Book { Title = "Szabad könyv", Author = "Szerző", Publisher = "Kiadó", ReleaseYear = 2000 });
        var book2 = await service.CreateAsync(new Book { Title = "Kölcsönzött könyv", Author = "Szerző", Publisher = "Kiadó", ReleaseYear = 2000 });

        db.Borrowings.Add(new Borrowing
        {
            ReaderId = 1,
            BookId = book2.Id,
            BorrowDate = DateTime.Today,
            DueDate = DateTime.Today.AddDays(14),
            IsReturned = false
        });
        await db.SaveChangesAsync();

        var result = await service.GetAvailableAsync();

        Assert.Single(result);
        Assert.Equal("Szabad könyv", result.First().Title);
    }

}