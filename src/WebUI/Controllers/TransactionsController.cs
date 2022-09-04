using DataAccess.Repositories;
using Domain.Common.DTOs;
using Domain.Common.DTOs.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebUI.Helpers.Extensions;

namespace WebUI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionRepository _transactionRepository;

    public TransactionsController(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<TransactionResponse>>> GetTransactions()
    {
        return Ok(await _transactionRepository.GetTransactionsAsync(User.GetUserId()));
    }
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TransactionResponse>> GetTransaction(int id)
    {
        return Ok(await _transactionRepository.GetTransactionAsync(id));
    }
    [HttpGet("filter")]
    public async Task<ActionResult<List<TransactionResponse>>> GetFilteredTransactions([FromQuery] TransactionParams @params)
    {
        return Ok(await _transactionRepository.GetFilteredTransactions(User.GetUserId(), @params));
    }
    [HttpPatch("{id:int}")]
    public async Task<ActionResult> CancelTransaction(int id)
    {
        var result = await _transactionRepository.CancelTransactionAsync(id);
        if (result)
            return Accepted();
        return BadRequest();
    }
    [HttpPost]
    public async Task<ActionResult<TransactionResponse>> Create(CreateTransactionDto model)
    {
        return Ok(await _transactionRepository.CreateTransactionAsync(User.GetUserId(), model));
    }
}
