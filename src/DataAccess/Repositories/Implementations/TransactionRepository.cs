using Domain.Common.DTOs;
using Domain.Common.DTOs.Responses;

namespace DataAccess.Repositories.Implementations;

internal class TransactionRepository : ITransactionRepository
{
    public Task<bool> CancelTransactionAsync(int userId, int id)
    {
        throw new NotImplementedException();
    }

    public Task<TransactionResponse> CreateTransactionAsync(int userId, CreateTransactionDto model)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TransactionResponse>> GetFilteredTransactions(int userId, TransactionParams @params)
    {
        throw new NotImplementedException();
    }

    public Task<TransactionResponse> GetTransactionAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TransactionResponse>> GetTransactionsAsync(int userId)
    {
        throw new NotImplementedException();
    }
}
