using System;

namespace API.Models.Dto;

public class StudentDto
{
    public required string FirstName { get; set; } 
    public string? LastName { get; set; } 
    public required string Email { get; set; } 
    public string? ProfilePictureUrl { get; set; } 

    public required string token { get; set; }
}
