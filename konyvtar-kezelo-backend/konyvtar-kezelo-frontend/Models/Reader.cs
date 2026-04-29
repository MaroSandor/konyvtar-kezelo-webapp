using System.ComponentModel.DataAnnotations;

namespace konyvtar_kezelo_frontend.Models
{
    public class Reader
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Az olvasó neve kötelező!")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "A lakcím kötelező!")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "A születési év kötelező!")]
        [Range(1900, 2026, ErrorMessage = "A születési év 1900 és 2026 között kell legyen!")]
        public int BirthYear { get; set; }
    }
}

