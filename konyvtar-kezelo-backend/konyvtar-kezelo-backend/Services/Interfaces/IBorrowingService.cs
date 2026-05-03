using konyvtar_kezelo_backend.Models;
using konyvtar_kezelo_backend.Models.DTOs;

namespace konyvtar_kezelo_backend.Services.Interfaces;

public interface IBorrowingService
{
    Task<IEnumerable<BorrowingResponseDto>> GetAllAsync();
    Task<BorrowingResponseDto?> GetByIdAsync(int id);
    Task<BorrowingResponseDto> CreateAsync(Borrowing borrowing);
    Task<BorrowingResponseDto?> UpdateAsync(int id, Borrowing borrowing);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<BorrowingResponseDto>> GetByReaderIdAsync(int readerId);
    Task<BorrowingResponseDto?> ReturnBookAsync(int id);
}