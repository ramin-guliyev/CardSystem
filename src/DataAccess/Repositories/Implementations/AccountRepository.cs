using AutoMapper;
using Domain.Common.DTOs;
using Domain.Common.DTOs.Responses;
using Domain.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace DataAccess.Repositories.Implementations;

internal class AccountRepository : IAccountRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    public AccountRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AccountResponse> CreateAccountAsync(int userId, CreateAccountDto model)
    {
        var account = new Account
        {
            Balance = model.Balance,
            Type = model.Type,
            User = new User { Id = userId }
        };

        _context.Accounts.Add(account);
        if(await _context.SaveChangesAsync()>0)
            return _mapper.Map<AccountResponse>(account);

        throw new Exception("Something went wrong");
    }

    public async Task<bool> DeleteAsync(int userId, int accountId)
    {
        var deletedEntity =await _context.Accounts
            .Where(x=>x.User.Id==userId).FirstOrDefaultAsync(x=>x.Id ==accountId);
        if (deletedEntity is null)
            throw new Exception("Invalid operation");
        _context.Accounts.Remove(deletedEntity);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<AccountResponse> GetAccountAsync(int userId, int id)
    {
        var account = await _context.Accounts
            .Where(x => x.User.Id == userId).FirstOrDefaultAsync(x => x.Id == id);

        if (account is null)
            throw new Exception("Invalid operation");
        return _mapper.Map<AccountResponse>(account);
    }

    public async Task<IEnumerable<AccountResponse>> GetAllAccountsAsync(int userId)
    {
        var accounts = await _context.Accounts
             .Where(x => x.User.Id == userId).ToListAsync();

        return _mapper.Map<List<AccountResponse>>(accounts);
    }

    public async Task<AccountResponse> UpdateAccountAsync(int userId, UpdateAccountDto model)
    {
        var updatedEntity = await _context.Accounts
            .Where(x => x.User.Id == userId).FirstOrDefaultAsync(x => x.Id == model.Id);
        if (updatedEntity is null)
            throw new Exception("Invalid operation");
        updatedEntity.Balance = model.Balance;
        updatedEntity.Type = model.Type;

        if (await _context.SaveChangesAsync() > 0)
            return _mapper.Map<AccountResponse>(updatedEntity);

        throw new Exception("Something went wrong");
    }
}
