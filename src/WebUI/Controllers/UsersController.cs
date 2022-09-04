using DataAccess.Repositories;
using Domain.Common.DTOs;
using Domain.Common.DTOs.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebUI.Helpers.Extensions;
using WebUI.Services;

namespace WebUI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IMailService _mailService;

    public UsersController(IUserRepository userRepository,IMailService mailService)
    {
        _userRepository = userRepository;
        _mailService = mailService;
    }
    [HttpPost("login")]
    public async Task<ActionResult<AuthenticationResponse>> Login(AuthenticationDto model)
    {
        var result = await _userRepository.AuthenticateAsync(model);
        return Ok(result);
    }
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<ActionResult<List<UserResponse>>> GetUsers()
    {
        return Ok(await _userRepository.GetUsersAsync());
    }
    [Authorize(Roles = "Admin")]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserResponse>> GetUser(int id)
    {
        return Ok(await _userRepository.GetUserAsync(id));
    }
    [Authorize(Roles = "Admin")]
    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterDto model)
    {
        var result = await _userRepository.RegisterAsync(model);
        if (result)
            return NoContent();
        return BadRequest();
    }

    [Authorize(Roles = "User")]
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
        var result = await _userRepository.ForgotPassword(model); 
        await _mailService.SendEmailAsync("","",result);
        return Accepted(new { token = result });//no need to return token back, just demo purpose
    }

    [HttpPost("reset-password")]
    public async Task<ActionResult> ResetPassword(ResetPasswordDto model)
    {
        var result = await _userRepository.ResetPassword(model);
        if (result)
            return Accepted();
        return BadRequest();
    }
    [Authorize]
    [HttpGet("logout")]
    public async Task<ActionResult> Logout()
    {
        await _userRepository.Logout();
        return Accepted();
    }
}
