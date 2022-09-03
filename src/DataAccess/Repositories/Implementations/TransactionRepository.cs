using AutoMapper;
using Domain.Common.DTOs;
using Domain.Common.DTOs.Responses;
using Domain.Data;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations;

internal class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;

    private readonly IMapper _mapper;

    public TransactionRepository(IMapper mapper, AppDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<bool> CancelTransactionAsync(int id)
    {
        var transaction = await _context.Transactions.FindAsync(id);
        if (transaction is null)
            throw new Exception("Invalid operation");

        transaction.Type = TransactionType.Cancelled;
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<TransactionResponse> CreateTransactionAsync(int userId, CreateTransactionDto model)
    {
        var transaction = new Transaction
        {
            Amount = model.Amount,
            CardNumber = model.CardNumber,
            Date = DateTime.UtcNow,
            Type = TransactionType.Normal,
            User = new() { Id = userId },
            Vendor = new Vendor
            {
                Email = model.Vendor.Email,
                Name = model.Vendor.Name,
                Phone = model.Vendor.Phone,
                Addresses = new List<Address>()
            }
        };
        model.Vendor.Addresses
            .ForEach(x => transaction.Vendor.Addresses.Add(new Address
            {
                City = x.City,
                Street = x.Street,
                ZipCode = x.ZipCode
            }));
        _context.Add(transaction);
        if (await _context.SaveChangesAsync() > 0)
            return _mapper.Map<TransactionResponse>(transaction);

        throw new Exception("Something went wrong");
    }

    public async Task<IEnumerable<TransactionResponse>> GetFilteredTransactions(int userId, TransactionParams @params)
    {
        var query = _context.Transactions
            .Include(x => x.User).Include(x => x.Vendor)
            .ThenInclude(x => x.Addresses).AsQueryable();

        query = query.Where(x => x.User.Id == userId);
        if (@params.MinAmount is not null)
            query = query.Where(x => x.Amount > @params.MinAmount);
        if (@params.CardNumber is not null)
            query = query.Where(x => x.CardNumber == @params.CardNumber);

        var filteredTransactions = await query.ToListAsync();
        return _mapper.Map<List<TransactionResponse>>(filteredTransactions);
    }

    public async Task<TransactionResponse> GetTransactionAsync(int id)
    {
        var transaction = await _context.Transactions
            .Include(x => x.User).Include(x => x.Vendor)
            .ThenInclude(x => x.Addresses).FirstOrDefaultAsync(x => x.Id == id);
        if (transaction is null)
            throw new Exception("Invalid operation");
        return _mapper.Map<TransactionResponse>(transaction);
    }

    public async Task<IEnumerable<TransactionResponse>> GetTransactionsAsync(int userId)
    {

        var transactions = await _context.Transactions
            .Include(x => x.User).Include(x => x.Vendor)
            .ThenInclude(x => x.Addresses).Where(x => x.User.Id == userId).ToListAsync();

        return _mapper.Map<List<TransactionResponse>>(transactions);
    }
}
