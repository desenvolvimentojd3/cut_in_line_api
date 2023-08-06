using CutInLine.Models.Class;
using CutInLine.Models.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CutInLine.Controllers;

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
    private readonly IUsers _user;

    public UserController(IUsers user)
    {
        _user = user;
    }

    [HttpPost]
    [Route("signup")]
    public async Task<dynamic> GetUsersToEvento([FromBody] Users user) => await _user.SignUp(user);
}
