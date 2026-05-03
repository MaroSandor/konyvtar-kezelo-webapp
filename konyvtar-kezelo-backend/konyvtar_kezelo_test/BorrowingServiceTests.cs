using konyvtar_kezelo_backend.Data;
using konyvtar_kezelo_backend.Models;
using konyvtar_kezelo_backend.Services;
using Microsoft.EntityFrameworkCore;

namespace konyvtar_kezelo_test;

public class BorrowingServiceTests
{
    private LibraryDBContext CreateDb()
    {
        var options = new DbContextOptionsBuilder<LibraryDBContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new LibraryDBContext(options);
    }

    [Fact]
    public async Task CreateAsync_ValidBorrowing_ReturnsBorrowing()
    {
        var db = CreateDb();
        var service = new BorrowingService(db);
        var borrowing = new Borrowing
        {
            ReaderId = 1,
            BookId = 1,
            BorrowDate = DateTime.Today,
            DueDate = DateTime.Today.AddDays(14)
        };

        var result = await service.CreateAsync(borrowing);

        Assert.NotNull(result);
        Assert.Equal(1, result.ReaderId);
    }

    [Fact]
    public async Task CreateAsync_DueDateBeforeBorrowDate_ThrowsException()
    {
        var db = CreateDb();
        var service = new BorrowingService(db);
        var borrowing = new Borrowing
        {
            ReaderId = 1,
            BookId = 1,
            BorrowDate = DateTime.Today,
            DueDate = DateTime.Today.AddDays(-1)
        };

        await Assert.ThrowsAsync<ArgumentException>(() => service.CreateAsync(borrowing));
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllBorrowings()
    {
        var db = CreateDb();
        var service = new BorrowingService(db);
        var borrowing = new Borrowing
        {
            ReaderId = 1,
            BookId = 1,
            BorrowDate = DateTime.Today,
            DueDate = DateTime.Today.AddDays(14)
        };
        await service.CreateAsync(borrowing);

        var result = await service.GetAllAsync();

        Assert.Single(result);
    }

    [Fact]
    public async Task GetByIdAsync_InvalidId_ReturnsNull()
    {
        var db = CreateDb();
        var service = new BorrowingService(db);

        var result = await service.GetByIdAsync(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteAsync_ValidId_ReturnsTrue()
    {
        var db = CreateDb();
        var service = new BorrowingService(db);
        var borrowing = new Borrowing
        {
            ReaderId = 1,
            BookId = 1,
            BorrowDate = DateTime.Today,
            DueDate = DateTime.Today.AddDays(14)
        };
        var created = await service.CreateAsync(borrowing);

        var result = await service.DeleteAsync(created.Id);

        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAsync_InvalidId_ReturnsFalse()
    {
        var db = CreateDb();
        var service = new BorrowingService(db);

        var result = await service.DeleteAsync(999);

        Assert.False(result);
    }
    [Fact]
    public async Task LateFee_NotLate_ReturnsZero()
    {
        var db = CreateDb();
        var service = new BorrowingService(db);
        var borrowing = new Borrowing
        {
            ReaderId = 1,
            BookId = 1,
            BorrowDate = DateTime.Today,
            DueDate = DateTime.Today.AddDays(5)
        };
        db.Borrowings.Add(borrowing);
        await db.SaveChangesAsync();

        var result = await service.GetByIdAsync(borrowing.Id);

        Assert.Equal(0, result!.LateFee);
    }

    [Fact]
    public async Task LateFee_5DaysLate_UsesMultiplier1()
    {
        var db = CreateDb();
        var service = new BorrowingService(db);
        var borrowing = new Borrowing
        {
            ReaderId = 1,
            BookId = 1,
            BorrowDate = DateTime.Today.AddDays(-10),
            DueDate = DateTime.Today.AddDays(-5)
        };
        db.Borrowings.Add(borrowing);
        await db.SaveChangesAsync();

        var result = await service.GetByIdAsync(borrowing.Id);

        // 650 * 5 * 1 = 3250
        Assert.Equal(3250, result!.LateFee);
    }

    [Fact]
    public async Task LateFee_12DaysLate_UsesMultiplier2()
    {
        var db = CreateDb();
        var service = new BorrowingService(db);
        var borrowing = new Borrowing
        {
            ReaderId = 1,
            BookId = 1,
            BorrowDate = DateTime.Today.AddDays(-20),
            DueDate = DateTime.Today.AddDays(-12)
        };
        db.Borrowings.Add(borrowing);
        await db.SaveChangesAsync();

        var result = await service.GetByIdAsync(borrowing.Id);

        // 650 * 12 * 2 = 15600
        Assert.Equal(15600, result!.LateFee);
    }

    [Fact]
    public async Task LateFee_20DaysLate_UsesMultiplier3()
    {
        var db = CreateDb();
        var service = new BorrowingService(db);
        var borrowing = new Borrowing
        {
            ReaderId = 1,
            BookId = 1,
            BorrowDate = DateTime.Today.AddDays(-30),
            DueDate = DateTime.Today.AddDays(-20)
        };
        db.Borrowings.Add(borrowing);
        await db.SaveChangesAsync();

        var result = await service.GetByIdAsync(borrowing.Id);

        // 650 * 20 * 3 = 39000
        Assert.Equal(39000, result!.LateFee);
    }
    [Fact]
    public async Task UpdateAsync_ValidId_ReturnsUpdatedBorrowing()
    {
        var db = CreateDb();
        var service = new BorrowingService(db);
        var borrowing = new Borrowing
        {
            ReaderId = 1,
            BookId = 1,
            BorrowDate = DateTime.Today,
            DueDate = DateTime.Today.AddDays(14)
        };
        var created = await service.CreateAsync(borrowing);

        var result = await service.UpdateAsync(created.Id, new Borrowing
        {
            ReaderId = 2,
            BookId = 1,
            BorrowDate = DateTime.Today,
            DueDate = DateTime.Today.AddDays(14)
        });

        Assert.NotNull(result);
        Assert.Equal(2, result.ReaderId);
    }

    [Fact]
    public async Task UpdateAsync_InvalidId_ReturnsNull()
    {
        var db = CreateDb();
        var service = new BorrowingService(db);

        var result = await service.UpdateAsync(999, new Borrowing
        {
            ReaderId = 1,
            BookId = 1,
            BorrowDate = DateTime.Today,
            DueDate = DateTime.Today.AddDays(14)
        });

        Assert.Null(result);
    }

    [Fact]

    public async Task GetByReaderIdAsync_ValidReaderId_ReturnsBookings()
    {
        var db = CreateDb();
        var service = new BorrowingService(db);

        var borrowing = new Borrowing
        {
            ReaderId = 1,
            BookId = 1,
            BorrowDate = DateTime.Today,
            DueDate = DateTime.Today.AddDays(14)
        };
        db.Borrowings.Add(borrowing);

        await db.SaveChangesAsync();

        var result = await service.GetByReaderIdAsync(1);


        Assert.Single(result);

    }

    [Fact]

    public async Task ReturnBookAsync_IsReturnedTrue()
    {
        var db = CreateDb();
        var service = new BorrowingService(db);

        var borrowing = new Borrowing
        {
            ReaderId = 1,
            BookId = 1,
            BorrowDate = DateTime.Today,
            DueDate = DateTime.Today.AddDays(14)
        };
        db.Borrowings.Add(borrowing);
        await db.SaveChangesAsync();

       var result =await service.ReturnBookAsync(borrowing.Id);

        Assert.True(result.IsReturned);

    }
}