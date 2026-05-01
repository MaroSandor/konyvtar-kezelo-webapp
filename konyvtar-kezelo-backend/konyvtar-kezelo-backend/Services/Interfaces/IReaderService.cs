using konyvtar_kezelo_backend.Models;

namespace konyvtar_kezelo_backend.Services.Interfaces;

public interface IReaderService
{
    Task<IEnumerable<Reader>> GetAllAsync();
    Task<Reader?> GetByIdAsync(int id);
    Task<Reader> CreateAsync(Reader reader);
    Task<Reader?> UpdateAsync(int id, Reader reader);
    Task<bool> DeleteAsync(int id);
}