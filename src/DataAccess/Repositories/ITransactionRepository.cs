using Domain.Common.DTOs;
using Domain.Common.DTOs.Responses;

namespace DataAccess.Repositories;

public interface ITransactionRepository
{
    Task<IEnumerable<TransactionResponse>> GetTransactionsAsync(int userId);
    Task<TransactionResponse> GetTransactionAsync(int id);    
    Task<TransactionResponse> CreateTransactionAsync(int userId,CreateTransactionDto model);
    Task<bool> CancelTransactionAsync(int userId,int id);
    Task<IEnumerable<TransactionResponse>> GetFilteredTransactions(int userId,TransactionParams @params);
}
