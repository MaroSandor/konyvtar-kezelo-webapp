using Microsoft.EntityFrameworkCore;
using konyvtar_kezelo_backend.Data;
using konyvtar_kezelo_backend.Models;
using konyvtar_kezelo_backend.Models.DTOs;
using konyvtar_kezelo_backend.Services.Interfaces;

namespace konyvtar_kezelo_backend.Services;

public class BorrowingService : IBorrowingService
{
    private readonly LibraryDBContext _context;
    private const decimal BaseFee = 650;
    
    public BorrowingService(LibraryDBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BorrowingResponseDto>> GetAllAsync() =>
        (await _context.Borrowings.ToListAsync()).Select(ToDto);

    public async Task<BorrowingResponseDto?> GetByIdAsync(int id)
    {
        var borrowing = await _context.Borrowings.FindAsync(id);
        return borrowing is null ? null : ToDto(borrowing);
    }

    public async Task<BorrowingResponseDto> CreateAsync(Borrowing borrowing)
    {
        if (borrowing.BorrowDate < DateTime.Today)
            throw new ArgumentException("A kölcsönzési idő nem lehet régebben mint a mai nap!");
        if (borrowing.DueDate <= borrowing.BorrowDate)
            throw new ArgumentException("A határidő nem lehet régebben mint a kölcsönzési idő!");
        
        _context.Borrowings.Add(borrowing);
        await _context.SaveChangesAsync();
        
        return ToDto(borrowing);
    }

    public async Task<BorrowingResponseDto?> UpdateAsync(int id, Borrowing borrowing)
    {
        var existing = await _context.Borrowings.FindAsync(id);
        if (existing is null)
        {
            return null;
        }
        
        existing.ReaderId = borrowing.ReaderId;
        existing.BookId = borrowing.BookId;
        existing.BorrowDate = borrowing.BorrowDate;
        existing.DueDate = borrowing.DueDate;
        
        _context.Borrowings.Update(existing);
        await _context.SaveChangesAsync();
        
        return ToDto(existing);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var borrowing = await _context.Borrowings.FindAsync(id);

        if (borrowing is null) return false;

        _context.Borrowings.Remove(borrowing);
        await _context.SaveChangesAsync();
        
        return true;
    }

    private static BorrowingResponseDto ToDto(Borrowing borrowing) => new()
    {
        Id = borrowing.Id,
        ReaderId = borrowing.ReaderId,
        BookId = borrowing.BookId,
        BorrowDate = borrowing.BorrowDate,
        DueDate = borrowing.DueDate,
        LateFee = CalculateLateFee(borrowing.DueDate),
    };

    private static decimal CalculateLateFee(DateTime dueDate)
    {
        var today = DateTime.Today;
        if (today <= dueDate)
        {
            return 0;
        }

        var daysLate = (today - dueDate).Days;
        var multiplier = daysLate switch
        {
            <= 10 => 1,
            <= 15 => 2,
            _ => 3
        };
        
        return BaseFee * daysLate * multiplier;
    }
}