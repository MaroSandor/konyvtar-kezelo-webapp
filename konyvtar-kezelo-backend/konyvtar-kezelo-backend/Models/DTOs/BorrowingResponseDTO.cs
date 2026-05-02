namespace konyvtar_kezelo_backend.Models.DTOs;

public class BorrowingResponseDto
{
    public int Id { get; set; }
    public int ReaderId { get; set; }
    public int BookId { get; set; }
    public DateTime BorrowDate { get; set; }
    public DateTime DueDate { get; set; }
    public decimal LateFee { get; set; }
    public bool
}