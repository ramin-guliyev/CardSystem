using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Common.DTOs;

public class CardDto
{
    [Required]
    public string Number { get; set; }
    [Required]
    public CardState State { get; set; }
    [Required]
    public CardType Type { get; set; }
    [Required]
    public CardCurrency Currency { get; set; }
}
