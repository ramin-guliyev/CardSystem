using Domain.Common.DTOs;
using Domain.Common.DTOs.Responses;

namespace DataAccess.Repositories.Implementations;

internal class UserRepository : IUserRepository
{
    public Task<AuthenticationResponse> AuthenticateAsync(AuthenticationDto model)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ChangeUserNameAsync(ChangeUserNameDto model)
    {
        throw new NotImplementedException();
    }

    public Task ForgotPassword(ForgotPasswordDto model)
    {
        throw new NotImplementedException();
    }

    public Task<UserResponse> GetUserAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserResponse>> GetUsersAsync()
    {
        throw new NotImplementedException();
    }

    public Task Logout()
    {
        throw new NotImplementedException();
    }

    public Task<bool> RegisterAsync(RegisterDto model)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ResetPassword(ResetPasswordDto model)
    {
        throw new NotImplementedException();
    }
}
