using Domain.Common.DTOs;
using Domain.Common.DTOs.Responses;

namespace DataAccess.Repositories.Implementations;

internal class AccountRepository : IAccountRepository
{
    public Task<AccountResponse> CreateAccountAsync(int userId, CreateAccountDto model)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int userId, int accountId)
    {
        throw new NotImplementedException();
    }

    public Task<AccountResponse> GetAccountAsync(int userId, int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<AccountResponse>> GetAllAccountsAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<AccountResponse> UpdateAccountAsync(int userId, UpdateAccountDto model)
    {
        throw new NotImplementedException();
    }
}
