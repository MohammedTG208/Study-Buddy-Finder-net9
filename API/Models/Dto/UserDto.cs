

using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace API.Models.Dto;

public class UserDto
{
    [NotNull]
    [StringLength(50, ErrorMessage = "First name must be less than 50 characters long.")]
    public required String FirstName { get; set; }

    
    [StringLength(50, ErrorMessage = "Last name must be less than 50 characters long.")]
    public String? LastName { get; set; }

    // Password must contain at least one uppercase letter, one lowercase letter, one digit, one special character and be between 8 and 16 characters long.
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,16}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, one special character and be between 8 and 16 characters long.")]
    public required String Password { get; set; }
    [EmailAddress(ErrorMessage = "Invalid email address format as XXXX@XXX.XX")]
    public required String Email { get; set; }

    [Url(ErrorMessage = "Invalid URL format")]
    public String? ProfilePictureUrl { get; set; } = null;

    public String? Role { get; set; } = "student"; // Default role for the user
}
