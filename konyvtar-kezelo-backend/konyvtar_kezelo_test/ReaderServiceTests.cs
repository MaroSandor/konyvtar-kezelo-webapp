using konyvtar_kezelo_backend.Data;
using konyvtar_kezelo_backend.Models;
using konyvtar_kezelo_backend.Services;
using Microsoft.EntityFrameworkCore;

namespace konyvtar_kezelo_test;

public class ReaderServiceTests
{
    private LibraryDBContext CreateDb()
    {
        var options = new DbContextOptionsBuilder<LibraryDBContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new LibraryDBContext(options);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllReaders()
    {
        var db = CreateDb();
        var service = new ReaderService(db);
        await service.CreateAsync(new Reader { Name = "Kiss János", Address = "Budapest", BirthDate = new DateOnly(1990, 1, 1) });
        await service.CreateAsync(new Reader { Name = "Nagy Éva", Address = "Debrecen", BirthDate = new DateOnly(1985, 5, 10) });

        var result = await service.GetAllAsync();

        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetByIdAsync_ValidId_ReturnsReader()
    {
        var db = CreateDb();
        var service = new ReaderService(db);
        var created = await service.CreateAsync(new Reader { Name = "Kiss János", Address = "Budapest", BirthDate = new DateOnly(1990, 1, 1) });

        var result = await service.GetByIdAsync(created.Id);

        Assert.NotNull(result);
        Assert.Equal("Kiss János", result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_InvalidId_ReturnsNull()
    {
        var db = CreateDb();
        var service = new ReaderService(db);

        var result = await service.GetByIdAsync(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_ValidReader_ReturnsReader()
    {
        var db = CreateDb();
        var service = new ReaderService(db);

        var result = await service.CreateAsync(new Reader { Name = "Kiss János", Address = "Budapest", BirthDate = new DateOnly(1990, 1, 1) });

        Assert.NotNull(result);
        Assert.Equal("Kiss János", result.Name);
    }

    [Fact]
    public async Task CreateAsync_BirthDateBefore1900_ThrowsException()
    {
        var db = CreateDb();
        var service = new ReaderService(db);

        await Assert.ThrowsAsync<ArgumentException>(() =>
            service.CreateAsync(new Reader { Name = "Öreg János", Address = "Budapest", BirthDate = new DateOnly(1899, 1, 1) }));
    }

    [Fact]
    public async Task UpdateAsync_ValidId_ReturnsUpdatedReader()
    {
        var db = CreateDb();
        var service = new ReaderService(db);
        var created = await service.CreateAsync(new Reader { Name = "Kiss János", Address = "Budapest", BirthDate = new DateOnly(1990, 1, 1) });

        var result = await service.UpdateAsync(created.Id, new Reader { Name = "Frissített Név", Address = "Pécs", BirthDate = new DateOnly(1990, 1, 1) });

        Assert.NotNull(result);
        Assert.Equal("Frissített Név", result.Name);
    }

    [Fact]
    public async Task UpdateAsync_InvalidId_ReturnsNull()
    {
        var db = CreateDb();
        var service = new ReaderService(db);

        var result = await service.UpdateAsync(999, new Reader { Name = "Valaki", Address = "Valahol", BirthDate = new DateOnly(1990, 1, 1) });

        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteAsync_ValidId_ReturnsTrue()
    {
        var db = CreateDb();
        var service = new ReaderService(db);
        var created = await service.CreateAsync(new Reader { Name = "Kiss János", Address = "Budapest", BirthDate = new DateOnly(1990, 1, 1) });

        var result = await service.DeleteAsync(created.Id);

        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAsync_InvalidId_ReturnsFalse()
    {
        var db = CreateDb();
        var service = new ReaderService(db);

        var result = await service.DeleteAsync(999);

        Assert.False(result);
    }
}