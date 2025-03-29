using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Interfaces;
using API.Models;
using Microsoft.IdentityModel.Tokens;

namespace API.Service;

public class TokenServiceImp(IConfiguration configuration) : ITokenService
{
    public string CreateToken(User user)
    {
        //1. Get the key token from the configuration file (appsettings.json)
        var keyToken= configuration["keyToken"]?? throw new Exception("Key token not found in configuration file");

        //2. Check if the key token is not null and has a length of at least 64 characters
        if(keyToken.Length < 64) throw new Exception("Key token is too short");


        //3. Create a symmetric security key using the key token from String to byte array
        //and then convert it to a SymmetricSecurityKey object
        var key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyToken));
        
        //4. Create a list of claims (user information) to be included in the token
        var claims=new List<Claim>{
            new (ClaimTypes.NameIdentifier,user.Email),
            new (ClaimTypes.Role,user.Role),
            
        };
        //5. Create a signing credentials object using the key and the HMAC SHA512 algorithm
        var creds=new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);

        //6. Create a security token descriptor object that contains the claims, expiration time, and signing credentials
        var tokenDescriptor=new SecurityTokenDescriptor{
            Subject=new ClaimsIdentity(claims),
            Expires=DateTime.UtcNow.AddDays(7),
            SigningCredentials=creds
        };

        //7. Create a token handler object to create the token
        var tokenHandler=new JwtSecurityTokenHandler();
        var token=tokenHandler.CreateToken(tokenDescriptor);

        //8. Write the token to a string using the token handler and return it
        return tokenHandler.WriteToken(token);
    }
}
