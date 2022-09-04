using AutoMapper;
using Domain.Common.DTOs;
using Domain.Common.DTOs.Responses;
using Domain.Data;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DataAccess.Repositories.Implementations;

internal class UserRepository : IUserRepository
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager;
    private readonly SignInManager<User> _signInManager;
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole<int>> roleManager, AppDbContext context, IMapper mapper, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _context = context;
        _mapper = mapper;
        _configuration = configuration;
    }
    public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationDto model)
    {
        var user = _context.Users.FirstOrDefault(x=>x.UserName==model.Username);
        if (user is  null)
            throw new Exception("User not found");

        var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, lockoutOnFailure: false);
        if (!result.Succeeded)
            throw new Exception($"Invalid password");

        user.LastLogin = DateTime.UtcNow;
        await _userManager.UpdateAsync(user);
        var token = await GenerateJWT(user);
        return new AuthenticationResponse
        {
            Token = token,
            UserName = user.UserName
        };
    }
    private async Task<string> GenerateJWT(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(_configuration["Secret"]);

        var userRoles = await _userManager.GetRolesAsync(user); //single role per user

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim("Email",user.Email),
                    new Claim(ClaimTypes.Role,userRoles[0])
            }),
            Expires = DateTime.UtcNow.AddHours(4),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);
        return tokenString;
    }
    public async Task<bool> ChangeUserNameAsync(int id, ChangeUserNameDto model)
    {
        var user = _context.Users.Find(id);
        if (user is null)
            throw new Exception("User not found"); //possible to throw custom exception

        user.UserName = model.UserName;
        user.Email = model.UserName;
        user.NormalizedUserName = model.UserName.ToUpper();
        user.NormalizedEmail = model.UserName.ToUpper();

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<string> ForgotPassword(ForgotPasswordDto model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);

        if (user is null)
            throw new Exception($"Wrong email address");

        return await _userManager.GeneratePasswordResetTokenAsync(user);
    }

    public async Task<UserResponse> GetUserAsync(int id)
    {
        var user = await _context.Users.Include(u => u.Cards).FirstOrDefaultAsync(x => x.Id == id);
        if (user is null)
            throw new Exception("User not found");
        return _mapper.Map<UserResponse>(user);
    }

    public async Task<IEnumerable<UserResponse>> GetUsersAsync()
    {
        var users = await _context.Users.Include(u => u.Cards).ToListAsync();
        return _mapper.Map<List<UserResponse>>(users);
    }

    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<bool> RegisterAsync(RegisterDto model)
    {
        var isUserAnyUser = await _userManager.FindByNameAsync(model.UserName);
        if (isUserAnyUser is not  null)
            throw new Exception("UserName already taken");

        User user = new()
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            UserName = model.UserName,
            Email = model.UserName
        };
        var result = await _userManager.CreateAsync(user, model.Password);
        await _userManager.AddToRoleAsync(user, "User");

        return result.Succeeded;
    }

    public async Task<bool> ResetPassword(ResetPasswordDto model)
    {
        var user = await _userManager.FindByNameAsync(model.UserName);
        if (user is null)
            throw new Exception("User not found");
        var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
        if (!result.Succeeded)
            throw new Exception("Something went wrong");

        user.LastPasswordChange = DateTime.UtcNow;
        var updateResult = await _userManager.UpdateAsync(user);

        return updateResult.Succeeded;
    }
}
