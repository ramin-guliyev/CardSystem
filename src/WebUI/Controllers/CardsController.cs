using DataAccess.Repositories;
using Domain.Common.DTOs;
using Domain.Common.DTOs.Responses;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize(Roles ="Admin")]
    [HttpGet]
    public async Task<ActionResult<List<CardResponse>>> GetAllCard()
    {
        return Ok(await _cardRepository.GetAllCardAsync());
    }

    [Authorize(Roles = "User")]
    [HttpGet("own-cards")]
    public async Task<ActionResult<List<CardResponse>>> GetOwnAllCard()
    {
        return Ok(await _cardRepository.GetAllCardAsync(User.GetUserId()));
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CardResponse>> GetCard(int id)
    {
        return Ok(await _cardRepository.GetCardAsync(id));
    }

    [Authorize(Roles = "User")]
    [HttpGet("own-card/{id:int}")]
    public async Task<ActionResult<CardResponse>> GetOwnCard(int id)
    {
        return Ok(await _cardRepository.GetCardAsync(id, User.GetUserId()));
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("create")]
    public async Task<ActionResult<CardResponse>> CreateCard(CardDto cardDto)
    {
        return Ok(await _cardRepository.CreateCardAsync(cardDto));
    }
    [Authorize(Roles = "Admin")]
    [HttpGet("add-to-card")]
    public async Task<ActionResult> AddToCard([FromQuery] int userId, [FromQuery] int cardId)
    {
        var result = await _cardRepository.AddUserToCardAsync(userId, cardId);
        if (result)
            return Accepted();
        return BadRequest();
    }
     [Authorize(Roles ="Admin")]
    [HttpGet("remove-from-card")]
    public async Task<ActionResult> RemoveCard([FromQuery] int userId, [FromQuery] int cardId)
    {
        var result = await _cardRepository.RemoveUserFromCardAsync(userId, cardId);
        if (result)
            return Accepted();
        return BadRequest();
    }
}
