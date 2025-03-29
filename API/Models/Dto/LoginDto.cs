
using System.ComponentModel.DataAnnotations;

namespace API.Models.Dto;

public class LoginDto
{
    [EmailAddress(ErrorMessage = "Invalid email address format as XXXX@XXX.XX")]
    public required String Email { get; set; }
    // Password must contain at least one uppercase letter, one lowercase letter, one digit, one special character and be between 8 and 16 characters long.
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,16}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, one special character and be between 8 and 16 characters long.")]
    public required String Password { get; set; }
}
