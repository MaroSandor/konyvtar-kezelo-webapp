using konyvtar_kezelo_backend.Models;

namespace konyvtar_kezelo_backend.Services.Interfaces;

public interface IBorrowingService
{
    Task<IEnumerable<BorrowingResponseDTO>> GetAllAsync();
    Task<BorrowingResponseDTO?> GetByIdAsync(int id);
    Task<BorrowingResponseDTO> CreateAsync(Borrowing borrowing);
    Task<BorrowingResponseDTO?> UpdateAsync(int id, Borrowing borrowing);
    Task<bool> DeleteAsync(int id);
}