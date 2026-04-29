using System.ComponentModel.DataAnnotations;

namespace konyvtar_kezelo_backend.Models;

public class Reader
{
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Address { get; set; }

    [Required]
    public DateTime BirthDate { get; set; }
    
    public ICollection<Borrowing> Borrowings { get; set; } = [];
}