using System.ComponentModel.DataAnnotations;

namespace konyvtar_kezelo_backend.Validation;

public class NoWhitespaceAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is string str)
        {
            return !string.IsNullOrWhiteSpace(str);
        }
        return true;
    }
}