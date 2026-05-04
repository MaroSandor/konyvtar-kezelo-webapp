using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace konyvtar_kezelo_frontend.Models;
public class Borrowing : IValidatableObject {
    public int Id { get; set; }
    public int LateFee { get; set; }
    [Required(ErrorMessage = "Reader ID is required!")]
    public int ReaderId { get; set; }

    [Required(ErrorMessage = "Book ID is required!")]
    public int BookId { get; set; }

    [Required(ErrorMessage = "Borrow date is required!")]
    public DateTime BorrowDate { get; set; } = DateTime.Today;

    [Required(ErrorMessage = "Return deadline is required!")]
    public DateTime DueDate { get; set; } = DateTime.Today.AddDays(14);

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (BorrowDate.Date < DateTime.Today)
        {
            yield return new ValidationResult(
                "Borrow date cannot be in the past!",
                new[] { nameof(BorrowDate) });
        }

        if (DueDate.Date <= BorrowDate.Date)
        {
            yield return new ValidationResult(
                "Return deadline must be later than the borrow date!",
                new[] { nameof(DueDate) });
        }
    }
}


