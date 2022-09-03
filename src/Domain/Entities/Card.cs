using Domain.Enums;

namespace Domain.Entities;

public class Card : BaseEntity
{
    public string Number { get; set; }
    public bool Valid { get; set; }
    public CardState State { get; set; }
    public CardType Type { get; set; }
    public CardCurrency Currency { get; set; }
    public User User { get; set; }
}
