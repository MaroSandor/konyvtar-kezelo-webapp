using System.ComponentModel.DataAnnotations;
using konyvtar_kezelo_backend.Validation;

namespace konyvtar_kezelo_backend.Models;

public class Reader
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Az olvasó neve kötelező!")]
    [NoWhitespace(ErrorMessage = "A mező nem tartalmazhat csak szóközöket!")]
    public string Name { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Az olvasó címe kötelező!")]
    [NoWhitespace(ErrorMessage = "A mező nem tartalmazhat csak szóközöket!")]
    public string Address { get; set; } = string.Empty;

    public DateOnly BirthDate { get; set; }
}