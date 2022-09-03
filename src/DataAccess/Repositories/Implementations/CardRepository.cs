using AutoMapper;
using Domain.Common.DTOs;
using Domain.Common.DTOs.Responses;
using Domain.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace DataAccess.Repositories.Implementations;

internal class CardRepository : ICardRepository
{
    private readonly AppDbContext _context;

    private readonly IMapper _mapper;
    public CardRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> AddUserToCardAsync(int userId, int cardId)
    {
        var card = await _context.Cards.FindAsync(cardId);
        if (card is null)
            throw new Exception("Invalid operation");

        var user = await _context.Users.FindAsync(userId);
        if (user is null)
            throw new Exception("Invalid operation");

        card.User = user;
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<CardResponse> CreateCardAsync(CardDto cardDto)
    {
        var card = new Card
        {
            Number = cardDto.Number,
            State = cardDto.State,
            Type = cardDto.Type,
            Currency = cardDto.Currency,
            Valid = true // dont know how to check this, think that CardNumber attribute will check successfully
        };
        _context.Cards.Add(card);
        if (await _context.SaveChangesAsync() > 0)
            return _mapper.Map<CardResponse>(card);

        throw new Exception("Something went wrong");
    }

    public async Task<IEnumerable<CardResponse>> GetAllCardAsync(int? userId = null)
    {
        if (userId is not null)
        {
            var userCards = await _context.Cards
                .Include(x => x.User).Where(x => x.User.Id == userId).ToListAsync();
            return _mapper.Map<List<CardResponse>>(userCards);
        }
        var cards = await _context.Cards.Include(x=>x.User).ToListAsync();
        return _mapper.Map<List<CardResponse>>(cards);
    }

    public async Task<CardResponse> GetCardAsync(int id, int? userId = null)
    {
        if (userId is not null)
        {
            var userCard = await _context.Cards
                .Include(x => x.User).Where(x => x.User.Id == userId).FirstOrDefaultAsync(x=>id==id);
            if (userCard is null)
                throw new Exception("Invalid operation");
            return _mapper.Map<CardResponse>(userCard);
        }

        var card = await _context.Cards.Include(x=>x.User).FirstOrDefaultAsync(x=>x.Id==id);
        if (card is null)
            throw new Exception("Invalid operation");
        return _mapper.Map<CardResponse>(card);
    }

    public async Task<bool> RemoveUserFromCardAsync(int userId, int cardId)
    {
        var card = await _context.Cards.FindAsync(cardId);
        if (card is null)
            throw new Exception("Invalid operation");

        card.User = null;
        return await _context.SaveChangesAsync() > 0;
    }
}
