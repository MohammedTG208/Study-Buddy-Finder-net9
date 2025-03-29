using API.Data;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class UsersController(DataContext context):BaseApiController
{

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers(){
        var users=await context.Users.ToListAsync();
        if(users==null || users.Count==0){
            return NotFound("No users found");
        }
        return Ok(users);
    }

    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<User>>> GetUser(int id){
        var user =await context.Users.FindAsync(id);

        // Check if the user is null and return a 404 Not Found response if it is.
        if(user==null){
            return NotFound("User not found");
        }
        return Ok(user);
    }
}
