using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace API.Controllers;



[ApiController]
[Route("api/[controller]")]//will display like localhost:5001/api/users
public class UsersController(DataContext context):ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers(){
        var users=await context.Users.ToListAsync();
        if(users==null) return NotFound("No users found");
        return Ok(users);
    }

    [HttpGet("{id:int}")] //will display like localhost:5001/api/users/{id}
    public async Task<ActionResult<User>> GetUser(int id){
        var user = await context.Users.FindAsync(id);
        if(user==null) return NotFound("Entity not found by this id : "+ id);
        return Ok(user);
    }
}
