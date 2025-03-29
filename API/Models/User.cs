namespace API.Models;

public class User
{
    public int Id { get; set; }

    public required String FirstName { get; set; }

    public String? LastName { get; set; }

    public required String Email { get; set; }

    public required byte[] PasswordHash { get; set; }

    public required byte[] PasswordSalt { get; set; }

    public String? ProfilePictureUrl { get; set; }=null;

    public DateTime CreatedAt { get; set; }=DateTime.Now;

    public DateTime? UpdateAt { get; set; }=null;

    public string Role { get; set; }="student"; // Default role for the user
}
