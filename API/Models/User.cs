namespace API.Models;

public class User
{
    public int Id { get; set; }

    public required String FirstName { get; set; }

    public String? LastName { get; set; }

    public required String Email { get; set; }

    public required string Password { get; set; }

    public String? ProfilePictureUrl { get; set; }=null;

    public DateTime CreatedAt { get; set; }=DateTime.Now;

    public DateTime? UpdateAt { get; set; }=null;
}
