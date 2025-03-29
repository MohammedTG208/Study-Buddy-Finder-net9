using System;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]//will display like localhost:5001/api/any controller name inherited from this class
public class BaseApiController :ControllerBase
{
}
