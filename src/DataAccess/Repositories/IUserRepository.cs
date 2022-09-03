using Domain.Common.DTOs;
using Domain.Common.DTOs.Responses;

namespace DataAccess.Repositories;

public interface IUserRepository
{
    Task<AuthenticationResponse> AuthenticateAsync(AuthenticationDto model);
    Task<IEnumerable<UserResponse>> GetUsersAsync();
    Task<UserResponse>GetUserAsync(int id);
    Task<bool> RegisterAsync(RegisterDto model);
    Task<bool> ChangeUserNameAsync(int id,ChangeUserNameDto model);
    Task<string> ForgotPassword(ForgotPasswordDto model);
    Task<bool> ResetPassword(ResetPasswordDto model);
    Task Logout();
}
