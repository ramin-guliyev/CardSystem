using Domain.Common.DTOs;
using Domain.Common.DTOs.Responses;

namespace DataAccess.Repositories;

public interface ICardRepository
{
    Task<IEnumerable<CardResponse>> GetAllCardAsync(int? userId=null);
    Task<CardResponse> GetCardAsync(int id,int? userId = null);
    Task<CardResponse> CreateCardAsync(CardDto cardDto);
    Task<bool> AddUserToCardAsync(int userId,int cardId);
    Task<bool> RemoveUserFromCardAsync(int userId,int cardId);
}
