using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.Interfaces;
using API.Models;
using API.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(DataContext context, ITokenService tokenService) : BaseApiController
{
    [HttpPost("register")]
    public async Task<ActionResult<StudentDto>> Register([FromBody] UserDto userDto)
    {
        if (await existsUser(userDto.Email))
        {
            return BadRequest("Email already exists");
        }

        using var hash = new HMACSHA512();

        var newUser = new User
        {
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            Email = userDto.Email.ToLower(),
            PasswordHash = hash.ComputeHash(Encoding.UTF8.GetBytes(userDto.Password)),
            PasswordSalt = hash.Key,
            ProfilePictureUrl = userDto.ProfilePictureUrl,
            Role = userDto.Role // Default role for the user
        };
        context.Users.Add(newUser);
        await context.SaveChangesAsync();
        return new StudentDto
        {
            FirstName = newUser.FirstName,
            LastName = newUser.LastName,
            Email = newUser.Email,
            ProfilePictureUrl = newUser.ProfilePictureUrl,
            token = tokenService.CreateToken(newUser)
        };


    }

    private async Task<bool> existsUser(String email)
    {
        // Check if a user with the given email already exists in the database.
        // If such a user is found, this variable will hold that bool value; otherwise, it will be false.
        // The search is case-insensitive.
        return await context.Users.AnyAsync(x => x.Email == email.ToLower());
    }


     [HttpPost("login")]
    public async Task<ActionResult<StudentDto>> Login([FromBody] LoginDto loginDto)
    {
        // Query the user by email and return default value as null if not found.
        var user = await context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email.ToLower());
        if (user == null)
        {
            return Unauthorized("Invalid email or password");
        }

        // Compute the hash using the stored salt.
        using var hmac = new HMACSHA512(user.PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        //compare the computed hash with the stored hash at DB.
        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash[i])
            {
                return Unauthorized("Invalid email or password");
            }
        }

        // Ideally. we should return a token here.
        return Ok(new StudentDto
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            ProfilePictureUrl = user.ProfilePictureUrl,
            token = tokenService.CreateToken(user)
        });
    }

}
