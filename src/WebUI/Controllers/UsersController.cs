using DataAccess.Repositories;
using Domain.Common.DTOs;
using Domain.Common.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;
using WebUI.Helpers.Extensions;

namespace WebUI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    [HttpPost("login")]
    public async Task<ActionResult<AuthenticationResponse>> Login(AuthenticationDto model)
    {
        var result = await _userRepository.AuthenticateAsync(model);
        return Ok(result);
    }
    [HttpGet]
    public async Task<ActionResult<List<UserResponse>>> GetUsers()
    {
        return Ok(await _userRepository.GetUsersAsync());
    }
    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserResponse>> GetUser(int id)
    {
        return Ok(await _userRepository.GetUserAsync(id));
    }
    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterDto model)
    {
        var result = await _userRepository.RegisterAsync(model);
        if (result)
            return NoContent();
        return BadRequest();
    }
    [HttpPost("change-username")]
    public async Task<ActionResult> ChangeUserName(ChangeUserNameDto model)
    {
        var result = await _userRepository.ChangeUserNameAsync(User.GetUserId(), model);
        if (result)
            return Accepted();
        return BadRequest();
    }
    [HttpPost("forgot-password")]
    public async Task<ActionResult> ForgotPassword(ForgotPasswordDto model)
    {
        var result = await _userRepository.ForgotPassword(model); //send email
                                                                  // if (result)
        return Accepted(result);
        return BadRequest();
    }

    [HttpPost("reset-password")]
    public async Task<ActionResult> ResetPassword(ResetPasswordDto model)
    {
        var result = await _userRepository.ResetPassword(model);
        if (result)
            return Accepted();
        return BadRequest();
    }
    [HttpGet("logout")]
    public async Task<ActionResult> Logout()
    {
        await _userRepository.Logout();
        return Accepted();
    }
}
