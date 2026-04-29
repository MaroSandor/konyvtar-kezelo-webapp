using System.ComponentModel.DataAnnotations;

namespace konyvtar_kezelo_frontend.Models;
public class Book{
    public int Id { get; set; }

    [Required(ErrorMessage = "A könyv címe kötelező!")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "A szerző kötelező!")]
    public string Author { get; set; } = string.Empty;

    [Required(ErrorMessage = "A kiadó kötelező!")]
    public string Publisher { get; set; } = string.Empty;

    [Required(ErrorMessage = "A kiadás éve kötelező!")]
    [Range(0, 2026, ErrorMessage = "A kiadás éve 0 és 2026 között kell legyen!")]
    public int ReleaseYear { get; set; }

}

