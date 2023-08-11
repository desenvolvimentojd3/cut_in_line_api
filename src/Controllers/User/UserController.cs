using CutInLine.Models.Class;
using CutInLine.Models.Interface;
using Microsoft.AspNetCore.Authorization;
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
    public async Task<dynamic> SignUp([FromBody] Users user) => await _user.SignUp(user);

    [HttpPost]
    [Route("signin")]
    public async Task<dynamic> SignIn([FromBody] Users user) => await _user.SignIn(user);

    [HttpGet]
    [Route("auth")]
    [Authorize]
    public dynamic Auth() => new { success = true };
}
