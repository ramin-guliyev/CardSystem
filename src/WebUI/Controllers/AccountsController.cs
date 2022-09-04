using DataAccess.Repositories;
using Domain.Common.DTOs;
using Domain.Common.DTOs.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebUI.Helpers.Extensions;

namespace WebUI.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly IAccountRepository _accountRepository;

    public AccountsController(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    [HttpGet]
    public async Task<ActionResult<List<AccountResponse>>> GetAccounts()
    {
        return Ok(await _accountRepository.GetAllAccountsAsync(User.GetUserId()));
    }
    [HttpGet("{id:int}")]
    public async Task<ActionResult<AccountResponse>> GetAccount(int id)
    {
        return Ok(await _accountRepository.GetAccountAsync(User.GetUserId(), id));
    }
    [HttpPost("create")]
    public async Task<ActionResult<AccountResponse>> CreateAccount(CreateAccountDto model)
    {
        return Ok(await _accountRepository.CreateAccountAsync(User.GetUserId(), model));
    }
    [HttpPost("update")]
    public async Task<ActionResult<AccountResponse>> UpdateAccount(UpdateAccountDto model)
    {
        return Ok(await _accountRepository.UpdateAccountAsync(User.GetUserId(), model));
    }
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _accountRepository.DeleteAsync(User.GetUserId(), id);
        if (result)
            return Accepted();
        return BadRequest();
    }
}
