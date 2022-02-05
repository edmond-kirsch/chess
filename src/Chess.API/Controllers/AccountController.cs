using Chess.API.Models;
using Chess.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Chess.API.Controllers;

[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var token = await _userService.GetUserTokenAsync(model.Username, model.Password);

        if (token != null)
        {
            return Ok(token);
        }

        return Unauthorized();
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var user = await _userService.GetUserAsync(model.Username);

        if (user != null)
        {
            return BadRequest();
        }

        var isUserCreated = await _userService.CreateUserAsync(model.Username, model.Email, model.Password);

        if (!isUserCreated)
        {
            return BadRequest();
        }

        return Ok();
    }
}