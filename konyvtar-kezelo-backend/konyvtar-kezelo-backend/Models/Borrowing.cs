using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace konyvtar_kezelo_backend.Models;

public class Borrowing
{
    public int Id { get; set; }

    public int ReaderId { get; set; }
    [JsonIgnore]
    public Reader? Reader { get; set; }
    
    public int BookId { get; set; }
    [JsonIgnore]
    public Book? Book { get; set; }
    
    public DateTime BorrowDate { get; set; }

    public DateTime DueDate { get; set; }
}