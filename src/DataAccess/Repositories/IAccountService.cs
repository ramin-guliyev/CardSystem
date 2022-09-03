using Domain.Common.DTOs;
using Domain.Common.DTOs.Responses;
namespace DataAccess.Repositories;

public interface IAccountRepository
{
    Task<IEnumerable<AccountResponse>> GetAllAccountsAsync(int userId);
    Task<AccountResponse> GetAccountAsync(int userId,int id);
    Task<AccountResponse> CreateAccountAsync(int userId,CreateAccountDto model);
    Task<AccountResponse> UpdateAccountAsync(int userId,UpdateAccountDto model);
    Task<bool> DeleteAsync(int userId,int accountId);
}
