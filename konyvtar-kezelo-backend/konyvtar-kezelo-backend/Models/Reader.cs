using System.ComponentModel.DataAnnotations;

namespace konyvtar_kezelo_backend.Models;

public class Reader
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Az olvasó neve kötelező!")]
    public string Name { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Az olvasó címe kötelező!")]
    public string Address { get; set; } = string.Empty;

    public DateOnly BirthDate { get; set; }
    
    public ICollection<Borrowing> Borrowings { get; set; } = [];
}