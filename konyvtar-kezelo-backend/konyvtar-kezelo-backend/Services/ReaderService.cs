using Microsoft.EntityFrameworkCore;
using konyvtar_kezelo_backend.Data;
using konyvtar_kezelo_backend.Models;
using konyvtar_kezelo_backend.Services.Interfaces;

namespace konyvtar_kezelo_backend.Services;

public class ReaderService : IReaderService
{
    private readonly LibraryDBContext _context;

    public ReaderService(LibraryDBContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Reader>> GetAllAsync() => await _context.Readers.ToListAsync();
    
    public async Task<Reader?> GetByIdAsync(int id) => await _context.Readers.FindAsync(id);

    public async Task<Reader> CreateAsync(Reader reader)
    {
        if (reader.BirthDate.Year < 1900)
        {
            throw new ArgumentException("A születési év 1900 előtti nem lehet!");
        }
        
        _context.Readers.Add(reader);
        await _context.SaveChangesAsync();

        return reader;
    }

    public async Task<Reader?> UpdateAsync(int id, Reader reader)
    {
        if (reader.BirthDate.Year < 1900)
        {
            throw new ArgumentException("A születési év 1900 előtti nem lehet!");
        }


        var existing = await _context.Readers.FindAsync(id);
        if (existing is null)
        {
            return null;
        }

        existing.Name = reader.Name;
        existing.Address = reader.Address;
        existing.BirthDate = reader.BirthDate;
        
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var reader = await _context.Readers.FindAsync(id);

        if (reader is null)
        {
            return false;
        }
        
        _context.Readers.Remove(reader);
        await _context.SaveChangesAsync();
        return true;
    }
}