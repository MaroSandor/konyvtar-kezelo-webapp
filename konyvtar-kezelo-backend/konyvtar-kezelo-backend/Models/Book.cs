using System.ComponentModel.DataAnnotations;

namespace konyvtar_kezelo_backend.Models;

public class Book
{
    public int Id { get; set; }
    
    [Required]
    public string Title { get; set; }
    
    [Required]
    public string Author { get; set; }
    
    [Required]
    public string Publisher { get; set; }
    
    [Range(0, int.MaxValue, ErrorMessage = "Release year can not be smaller than zero.")]
    public int ReleaseYear { get; set; }

    public ICollection<Borrowing> Borrowings { get; set; } = [];
}