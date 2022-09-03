using Domain.Common.DTOs;
using Domain.Common.DTOs.Responses;

namespace DataAccess.Repositories.Implementations;

internal class CardRepository : ICardRepository
{
    public Task<bool> AddUserToCardAsync(int userId, int cardId)
    {
        throw new NotImplementedException();
    }

    public Task<CardResponse> CreateCardAsync(CardDto cardDto)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CardResponse>> GetAllCardAsync(int? userId = null)
    {
        throw new NotImplementedException();
    }

    public Task<CardResponse> GetCardAsync(int id, int? userId = null)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveUserFromCardAsync(int userId, int cardId)
    {
        throw new NotImplementedException();
    }
}
