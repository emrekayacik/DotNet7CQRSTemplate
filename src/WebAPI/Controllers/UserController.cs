using Application.Business.User.Commands;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Base;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ApiControllerBase
{
    private IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateUserCommand command)
    {

        return await Mediator.Send(command);
    }

    [HttpPost("Login")]
    public IActionResult Authenticate(AuthenticateRequest model)
    {
        var response = _userService.Authenticate(model);

        if (response == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        return Ok(response);
    }
    [HttpGet("GetUser")]
    public async Task<ActionResult<User>> GetUser(Guid Id)
    {
        var response = await _userService.GetUserAsync(Id);

        if (response == null)
            return NotFound(new { message = "User Cannot Found" });

        return Ok(response);
    }
    [HttpGet("GetAllUsers")]
    public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
    {
        var response = await _userService.GetAllUsersAsync();

        if (response == null)
            return NotFound(new { message = "No Users Found" });

        return Ok(response);
    }

}
