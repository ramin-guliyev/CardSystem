using DataAccess.Repositories;
using Domain.Common.DTOs;
using Domain.Common.DTOs.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebUI.Helpers.Extensions;

namespace WebUI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CardsController : ControllerBase
{
    private readonly ICardRepository _cardRepository;

    public CardsController(ICardRepository cardRepository)
    {
        _cardRepository = cardRepository;
    }
    [HttpGet]
    public async Task<ActionResult<List<CardResponse>>> GetAllCard()
    {
        return Ok(await _cardRepository.GetAllCardAsync());
    }
    [HttpGet("own-cards")]
    public async Task<ActionResult<List<CardResponse>>> GetOwnAllCard()
    {
        return Ok(await _cardRepository.GetAllCardAsync(User.GetUserId()));
    }
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CardResponse>> GetCard(int id)
    {
        return Ok(await _cardRepository.GetCardAsync(id));
    }
    [HttpGet("own-card/{id:int}")]
    public async Task<ActionResult<CardResponse>> GetOwnCard(int id)
    {
        return Ok(await _cardRepository.GetCardAsync(id, User.GetUserId()));
    }
    [HttpPost("create")]
    public async Task<ActionResult<CardResponse>> CreateCard(CardDto cardDto)
    {
        return Ok(await _cardRepository.CreateCardAsync(cardDto));
    }
    [HttpGet("add-to-card")]
    public async Task<ActionResult> AddToCard([FromQuery] int userId, [FromQuery] int cardId)
    {
        var result = await _cardRepository.AddUserToCardAsync(userId, cardId);
        if (result)
            return Accepted();
        return BadRequest();
    }
    [HttpGet("remove-from-card")]
    public async Task<ActionResult> RemoveCard([FromQuery] int userId, [FromQuery] int cardId)
    {
        var result = await _cardRepository.RemoveUserFromCardAsync(userId, cardId);
        if (result)
            return Accepted();
        return BadRequest();
    }
}
