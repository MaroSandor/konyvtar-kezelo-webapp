using System.ComponentModel.DataAnnotations;

namespace konyvtar_kezelo_backend.Models;

public class Book
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "A könyv címe kötelező!")]
    public string Title { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "A szerző neve kötelező!")]
    public string Author { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "A kiadó neve kötelező!")]
    public string Publisher { get; set; } = string.Empty;
    
    [Range(0, int.MaxValue, ErrorMessage = "Kiadási dátum nem lehet kisebb mint 0.")]
    public int ReleaseYear { get; set; }

    public ICollection<Borrowing> Borrowings { get; set; } = [];
}