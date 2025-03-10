using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]//will display like localhost:5001/api/users
public class UsersControllers(DataContext context):ControllerBase
{

    [HttpGet]
    public ActionResult<IEnumerable<User>> GetUsers(){
        var users=context.Users.ToList;
        return Ok(users);
    }

    [HttpGet("{id:int}")]
    public ActionResult<IEnumerable<User>> GetUser(int id){
        var user = context.Users.Find(id);
        return Ok(user);
    }
}
