using konyvtar_kezelo_backend.Data;
using konyvtar_kezelo_backend.Models;
using konyvtar_kezelo_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace konyvtar_kezelo_backend.Services;

public class BookService : IBookService
{
    private readonly LibraryDBContext _context;
    
    public BookService(LibraryDBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Book>> GetAllAsync() => await _context.Books.ToListAsync();

    public async Task<Book?> GetByIdAsync(int id) => await _context.Books.FindAsync(id);

    public async Task<Book> CreateAsync(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task<Book?> UpdateAsync(int id, Book book)
    {
        var existing = await _context.Books.FindAsync(id);
        if (existing is null) return null;
        
        existing.Title = book.Title;
        existing.Author = book.Author;
        existing.Publisher = book.Publisher;
        existing.ReleaseYear = book.ReleaseYear;
        
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book is null) return false;

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Book>> GetAvailableAsync() => await _context.Books
        .Where(book => !book.Borrowings.Any(br => !br.IsReturned))
        .ToListAsync();
}